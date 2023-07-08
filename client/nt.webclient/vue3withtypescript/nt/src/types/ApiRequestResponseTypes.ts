export interface IResponseBase{
    hasError : boolean,
    status? : number,
    errors? : Array<string>
}
export interface IRegisterUserRequest{
    userName : string,
    displayName? : string,
    password : string,
}

export interface IRegisterUserResponse extends IResponseBase{
    data : {
        userName : string
    }
}

export interface IValidateUserRequest {
    userName :string,
    password : string
}


export interface IValidateUserResponse extends IResponseBase{
    token : string,
    isAuthenticated : boolean,
    loginTime : Date,
    userName : string
}

