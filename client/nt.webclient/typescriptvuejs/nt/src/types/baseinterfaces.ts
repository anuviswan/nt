import {ValidationError} from "./basetypes"

export interface ITypeValidate{
    Validate():ValidationError;
}

export interface ServerResponse{
    Message:string;
    HasError:boolean;
}