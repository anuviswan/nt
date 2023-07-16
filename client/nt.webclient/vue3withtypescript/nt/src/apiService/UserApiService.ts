import { IRegisterUserRequest, IRegisterUserResponse, IValidateUserRequest, IValidateUserResponse} from "../types/ApiRequestResponseTypes";
import{AxiosResponse} from "axios";
import { ApiServiceBase } from "./ApiServiceBase";

class UserApiService extends ApiServiceBase {

    public async validateUser(user:IValidateUserRequest):Promise<IValidateUserResponse>{
        return await this.invoke<IValidateUserResponse>({method:'post', url:"/api/User/ValidateUser", data:  user});
    }

    public async registerUser(user:IRegisterUserRequest):Promise<IRegisterUserResponse>{
        
        return await this.invoke<IRegisterUserResponse,AxiosResponse<IRegisterUserResponse>>({method:'post', url:"api/user/createuser", data : user});
    }
}

export const userApiService = new UserApiService();