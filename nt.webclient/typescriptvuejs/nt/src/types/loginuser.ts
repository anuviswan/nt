import {ITypeValidate} from './baseinterfaces';
import {ValidationError} from "./basetypes"

export class LoginUser implements ITypeValidate{
    public userName:string;
    public password:string;

    constructor() {
        this.userName = '';
        this.password = '';        
    }
    public Validate():ValidationError{
        let errors : Array<string>;
        errors = [];
        
        if (!this.userName) {
            errors.push("userName");
        }

        if (!this.password) {
            errors.push("password");
        }

        return new ValidationError(errors);
    }
}