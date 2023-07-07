import { IRegisterUserRequest, IRegisterUserResponse} from "../types/ApiRequestResponseTypes";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosResponse} from "axios";
import HttpApiService from "./HttpApiService";
//import {registerUser} from "@/api/user.js"



class UserApiService extends HttpApiService {
    
    public validateUser():boolean{

        return true;
    }

    public async registerUser(user:IRegisterUserRequest):Promise<IRegisterUserResponse>{
        
        const response = await this.invoke<IRegisterUserResponse,AxiosResponse<IRegisterUserResponse>>({method:'post', url:"/user/createuser", data : user});
        return response;
    }
}

export const userApiService = new UserApiService();