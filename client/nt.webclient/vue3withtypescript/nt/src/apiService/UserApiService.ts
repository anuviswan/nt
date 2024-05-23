import { IChangePasswordRequest, IChangePasswordResponse, IGetProfileImageRequest, IGetProfileImageResponse, IRegisterUserRequest, IRegisterUserResponse, IUploadProfileImageRequest, IUploadProfileImageResponse, IValidateUserRequest, IValidateUserResponse} from "../types/apirequestresponsetypes/User";
import{AxiosResponse} from "axios";
import { ApiServiceBase } from "./ApiServiceBase";

class UserApiService extends ApiServiceBase {

    public async validateUser(user:IValidateUserRequest):Promise<IValidateUserResponse>{
        return await this.invoke<IValidateUserResponse>({method:'post', url:"/api/User/ValidateUser", data:  user});
    }

    public async registerUser(user:IRegisterUserRequest):Promise<IRegisterUserResponse>{
        
        return await this.invoke<IRegisterUserResponse,AxiosResponse<IRegisterUserResponse>>({method:'post', url:"api/user/createuser", data : user});
    }

    public async changePassword(request:IChangePasswordRequest):Promise<IChangePasswordResponse>{
        return await this.invoke<IChangePasswordResponse,AxiosResponse<IChangePasswordResponse>>({method:'post', url:"/api/user/ChangePassword", data : request});
    }

    public async uploadProfileImage(request:IUploadProfileImageRequest):Promise<IUploadProfileImageResponse>{

        const formData = new FormData();
        formData.append('imageKey',request.imageKey);
        formData.append('file', request.file);

        return await this.invoke<IUploadProfileImageResponse,AxiosResponse<IUploadProfileImageResponse>>(
            {
                method:'post', 
                url:"user/api/Users/uploadprofileimage", 
                data : formData,
                headers: { "Content-Type": "multipart/form-data" }
            });
    }

    public async getProfileImage(request:IGetProfileImageRequest):Promise<IGetProfileImageResponse>{
        return await this.invoke<IGetProfileImageResponse,AxiosResponse<IGetProfileImageResponse>>({
            method:'get',
            'url':'',
            data:request
        })
    }

}

export const userApiService = new UserApiService();