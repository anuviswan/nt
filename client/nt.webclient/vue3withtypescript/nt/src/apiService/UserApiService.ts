import { IChangePasswordRequest, IChangePasswordResponse, IRegisterUserRequest, IRegisterUserResponse, IUploadProfileImageRequest, IUploadProfileImageResponse, IValidateUserRequest, IValidateUserResponse} from "../types/apirequestresponsetypes/User";
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
        return await this.invoke<IUploadProfileImageResponse,AxiosResponse<IUploadProfileImageResponse>>(
            {
                method:'post', 
                url:"api/user/uploadprofileimage", 
                data : request,
                headers: { "Content-Type": "multipart/form-data" }
            });
    }
}

export const userApiService = new UserApiService();