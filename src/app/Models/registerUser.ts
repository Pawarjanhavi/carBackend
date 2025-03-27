export class registerModel{
    firstName:string;
    lastName:string;
    email:string;
    password:string;
    phoneNumber:string;
    roleType:string[];

    constructor(){
        this.firstName ='';
        this.lastName ='';
        this.email ='';
        this.password ='';
        this.phoneNumber='';
        this.roleType = [];
    }
}