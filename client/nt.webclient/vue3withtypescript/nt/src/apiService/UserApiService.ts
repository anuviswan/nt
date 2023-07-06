//import { RegisterUserRequest, RegisterUserResponse} from "./userType";
import axios, {AxiosInstance, AxiosRequestConfig} from "axios";
import HttpApiService from "./HttpApiService";
//import {registerUser} from "@/api/user.js"


interface RegisterUserRequest{
    userName : string,
    displayName : string,
    password : string,
}

interface RegisterUserResponse{
    
}

class UserApiService extends HttpApiService {
    
    public validateUser():boolean{

        return true;
    }

    public registerUser(user:RegisterUserRequest):boolean{
        this.invoke<RegisterUserResponse>({method:'post', url:"/user/createuser", data : user});
        return false;
    }
}

export default UserApiService;