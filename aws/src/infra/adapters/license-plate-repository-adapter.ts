import {DynamoDB} from 'aws-sdk';
import {LicensePlateRegistration, LicensePlateType} from "../../domain/LicensePlateService/license-plate-registration";

const documentClient: DynamoDB.DocumentClient = new DynamoDB.DocumentClient();

async function resolveTableName(): Promise<string> {
    if (!process.env.LICENSE_PLATE_TABLE_NAME) {
        return Promise.reject('env property LICENSE_PLATE_TABLE_NAME not found.');
    }
    return process.env.LICENSE_PLATE_TABLE_NAME.toString();
}

export class LicensePlateRepositoryAdapter {

    public static async findLicensePlate(license: string): Promise<LicensePlateRegistration> {
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
                console.log("data: "+data.Count);
                if (data.Count && data.Count > 0 && data.Items) {
                    console.log("data.items");
                    return Promise.resolve(LicensePlateRegistration.fromJSON(data.Items.pop() as any));
                }
                console.log("nothing...");
                return Promise.resolve(new LicensePlateRegistration(license, LicensePlateType.UNKNOWN));
            },
            error => {
                return Promise.reject(error.message)
            }
        );

    }

}