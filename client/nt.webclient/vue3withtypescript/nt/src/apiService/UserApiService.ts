import { IRegisterUserRequest, IRegisterUserResponse} from "../types/ApiRequestResponseTypes";
import{AxiosResponse} from "axios";
import { ApiServiceBase } from "./ApiServiceBase";

class UserApiService extends ApiServiceBase {

    public validateUser():boolean{

        return true;
    }

    public async registerUser(user:IRegisterUserRequest):Promise<IRegisterUserResponse>{
        
        const response = await this.invoke<IRegisterUserResponse,AxiosResponse<IRegisterUserResponse>>({method:'post', url:"/user/createuser", data : user});
        return response;
    }
}

export const userApiService = new UserApiService();