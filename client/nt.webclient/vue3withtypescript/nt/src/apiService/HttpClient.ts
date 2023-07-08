import { IResponseBase } from "@/types/ApiRequestResponseTypes";
import axios, {AxiosInstance, AxiosRequestConfig, AxiosError, AxiosResponse} from "axios";

class HttpClient{

    private axiosInstance : AxiosInstance;
    
    constructor(){
        const headers = {
            "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
            "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
            "Content-Type": "application/json", // this shows the expected content type
          };
        this.axiosInstance = axios.create({baseURL: "http://localhost:8001/", headers:headers});
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
            }
            else{
                console.log("Some other error ?? " + error);
            }
        } 

        return <T>{};

        
    }
}

export default HttpClient;