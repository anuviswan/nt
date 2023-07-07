import { IResponseBase } from "@/types/ApiRequestResponseTypes";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosError, AxiosResponse} from "axios";

class HttpApiService{
    private instance : AxiosInstance;
    
    constructor(){
        this.instance = axios.create({baseURL: "http://localhost:8001/"});
    }

    protected async invoke<T extends IResponseBase,R = AxiosResponse<T>>(config:AxiosRequestConfig):Promise<T> {

        try{
            const response =  await this.instance.request<T>(config); 
            return response.data;
        }catch(error : AxiosError | any){

            if(axios.isAxiosError(error)){
                return <T>{
                    status : error.response?.status,
                    errors : error.response?.data.errors
                } 
                console.log(error.response?.status)
            }
            else{
                console.log("Some other error ?? " + error);
            }
        } 

        return <T>{};

        
    }
}

export default HttpApiService;