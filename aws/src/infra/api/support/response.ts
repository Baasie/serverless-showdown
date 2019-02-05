
export class Response {

    public static OK = (body: string) => new Response(200, body);
    public static BAD_REQUEST = (body: string) => new Response(400, body);
    public static CONFLICT = (body: string) => new Response(409, body);
    public static INTERNAL_SERVER_ERROR = (body: string) => new Response(500, body);

    private headers: { [name: string]: string } = {};

    constructor(public readonly statusCode: number, public readonly body: string) {

    }
}



