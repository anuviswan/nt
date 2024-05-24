export interface IResponseBase {
    hasError: boolean,
    status?: number,
    errors?: Array<string>
}
export interface IRegisterUserRequest {
    userName: string,
    displayName?: string,
    password: string,
}

export interface IRegisterUserResponse extends IResponseBase {
    data: {
        userName: string
    }
}

export interface IValidateUserRequest {
    userName: string,
    password: string
}


export interface IValidateUserResponse extends IResponseBase {
    data: {
        token: string,
        isAuthenticated: boolean,
        loginTime: Date,
        userName: string,
        displayName? : string,
        bio? : string
    }
}

export interface IChangePasswordRequest {
    oldPassword : string,
    newPassword : string
}

export interface IChangePasswordResponse extends IResponseBase{
    data: {
        
    }
}

export interface IUploadProfileImageRequest{
    imageKey : string,
    file: File 
}

export interface IUploadProfileImageResponse extends IResponseBase{
    data: {
        
    }
}

export interface IGetProfileImageRequest{
    userName : string
}

export interface IGetProfileImageResponse extends IResponseBase{
    file:Blob
}


