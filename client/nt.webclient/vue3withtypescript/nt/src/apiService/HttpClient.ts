import { IResponseBase } from "@/types/ApiRequestResponseTypes";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosError, AxiosResponse} from "axios";

class HttpClient{

    private axiosInstance : AxiosInstance;
    
    constructor(){
        this.axiosInstance = axios.create({baseURL: "http://localhost:8001/"});
    }

    public async invoke<T extends IResponseBase,R = AxiosResponse<T>>(config:AxiosRequestConfig):Promise<T> {

        try{
            const response =  await this.axiosInstance.request<T>(config); 
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

export default HttpClient;