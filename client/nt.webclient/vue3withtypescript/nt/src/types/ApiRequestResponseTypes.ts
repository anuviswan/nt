export interface IResponseBase{
    status? : number,
    errors? : Array<string>
}
export interface IRegisterUserRequest{
    userName : string,
    displayName? : string,
    password : string,
}

export interface IRegisterUserResponse extends IResponseBase{
    status : number,
    data : {
        userName : string
    }
}