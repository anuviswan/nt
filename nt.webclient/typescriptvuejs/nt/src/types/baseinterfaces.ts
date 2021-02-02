import {ValidationError} from "./basetypes"

export interface ITypeValidate{
    Validate():ValidationError;
}