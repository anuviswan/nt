import {IValidate} from './baseinterfaces';

export class LoginUser implements IValidate{
    public userName:string;
    public password:string;

    constructor() {
        this.userName = '';
        this.password = '';        
    }
    public Validate():boolean{
        return false;
    }
}