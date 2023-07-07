import { IResponseBase } from "@/types/ApiRequestResponseTypes";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosError} from "axios";

class HttpApiService{
    private instance : AxiosInstance;
    
    constructor(){
        this.instance = axios.create({baseURL: "http://localhost:8001/"});
    }

    protected async invoke<T>(config:AxiosRequestConfig):Promise<IResponseBase> {

        try{
            return await this.instance.request<T>(config); 
        }catch(error : AxiosError | any){

            if(axios.isAxiosError(error)){
                return {
                    status : error.response?.status,
                    errors : error.response?.data.errors
                }
                console.log(error.response?.status)
            }
            else{
                console.log("Some other error ?? " + error);
            }
        } 

        return {};

        
    }
}

export default HttpApiService;