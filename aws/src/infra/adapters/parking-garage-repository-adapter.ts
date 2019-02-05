import {ParkingGarage} from "../../domain/ParkingGarage/parking-garage";
import {DynamoDB} from 'aws-sdk';
import {PutItemInput, PutItemInputAttributeMap} from 'aws-sdk/clients/dynamodb';


const documentClient: DynamoDB.DocumentClient = new DynamoDB.DocumentClient();

async function resolveTableName(): Promise<string> {
    if (!process.env.PARKING_GARAGE_TABLE_NAME) {
        return Promise.reject('env property PARKING_GARAGE_TABLE_NAME not found.');
    }
    return process.env.PARKING_GARAGE_TABLE_NAME.toString();
}

export class ParkingGarageRepositoryAdapter {

    public static async saveParkingGarage(parkingGarage: ParkingGarage): Promise<ParkingGarage> {
        const tableName = await resolveTableName();
        const params: PutItemInput = {
            TableName: tableName,
            Item: parkingGarage.toJSON() as PutItemInputAttributeMap,
        };
        return documentClient.put(params).promise().then(
            data => Promise.resolve(parkingGarage),
            error => Promise.reject(error)
        );

    }

    public static async findParkingGarage(id: string): Promise<ParkingGarage> {
        const tableName = await resolveTableName();

        const params = {
            TableName: tableName,
            KeyConditionExpression: '#id = :id',
            ExpressionAttributeNames: {
                '#id': 'id',
            },
            ExpressionAttributeValues: {
                ':id': id,
            }
        };

        let data = await documentClient.query(params).promise();
        if (data.Items) {
            return Promise.resolve(ParkingGarage.fromJSON(data.Items.pop() as any));
        }
        return Promise.reject('no parking garage found');

    }
}