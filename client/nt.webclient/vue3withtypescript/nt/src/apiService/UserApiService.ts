import { IRegisterUserRequest, IRegisterUserResponse} from "../types/ApiRequestResponseTypes";
import axios, {AxiosInstance, AxiosRequestConfig} from "axios";
import HttpApiService from "./HttpApiService";
//import {registerUser} from "@/api/user.js"



class UserApiService extends HttpApiService {
    
    public validateUser():boolean{

        return true;
    }

    public async registerUser(user:IRegisterUserRequest):Promise<boolean>{
        try{
            const response = await this.invoke<IRegisterUserResponse>({method:'post', url:"/user/createuser", data : user});
            console.log("response from api " + response)
            console.log(response)
        }catch(e){
            console.log("error");
            console.log(e);
        }
        
        return false;
    }
}

export const userApiService = new UserApiService();