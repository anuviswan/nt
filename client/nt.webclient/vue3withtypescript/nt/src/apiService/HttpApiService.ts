import axios, {AxiosInstance, AxiosRequestConfig} from "axios";

class HttpApiService{
    private instance : AxiosInstance;
    
    constructor(){
        this.instance = axios.create({baseURL: "http://localhost:8001/"});
    }

    protected async invoke<T>(config:AxiosRequestConfig) {
        const {data /*, status*/ } = await this.instance.request<T>(config); 
        return data;
    }
}

export default HttpApiService;