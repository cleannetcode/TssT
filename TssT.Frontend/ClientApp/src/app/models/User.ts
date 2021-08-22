export class User
{
    id: number;
    login: any;
    password: any;
    username: any;
    token: any;
    image: any;

    constructor(id?:number, login?:string, token?:string) {
        this.id = id ?? 0;
        this.image = '';
        this.login = login;
        this.token = token;
    }
}
