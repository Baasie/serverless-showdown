import {DynamoDB} from 'aws-sdk';
import {LicensePlate, LicensePlateType} from "../../domain/license-plate";

const documentClient: DynamoDB.DocumentClient = new DynamoDB.DocumentClient();

async function resolveTableName(): Promise<string> {
    if (!process.env.LICENSE_PLATE_TABLE_NAME) {
        return Promise.reject('env property LICENSE_PLATE_TABLE_NAME not found.');
    }
    return process.env.LICENSE_PLATE_TABLE_NAME.toString();
}

export class LicensePlateRepository {

    public static async findLicensePlate(license: string): Promise<LicensePlate> {
        console.log("findLicensePlate with "+license);
        let tableName = await resolveTableName();
        console.log("tableName: "+tableName);
        let params = {
            TableName: tableName,
            KeyConditionExpression: '#license = :license',
            ExpressionAttributeNames: {
                '#license': 'license',
            },
            ExpressionAttributeValues: {
                ':license': license,
            }
        };

        return documentClient.query(params).promise().then(
            data => {
                console.log("data: "+data.Items);
                if (data.Items) {
                    console.log("data.items");
                    return Promise.resolve(LicensePlate.fromJSON(data.Items.pop() as any));
                }
                console.log("nothing...");
                return Promise.resolve(new LicensePlate(license, LicensePlateType.UNKNOWN));
            },
            error => {
                return Promise.reject(error.message)
            }
        );

    }

}