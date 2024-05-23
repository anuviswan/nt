import { IResponseBase } from "@/types/apirequestresponsetypes/User";
import { AxiosRequestConfig, AxiosResponse } from "axios";
import HttpClient from "./HttpClient";


export abstract class ApiServiceBase
{
    private httpClient : HttpClient;

    constructor(){
        this.httpClient = new HttpClient();
    }

    protected async invoke<T extends IResponseBase>(request:AxiosRequestConfig):Promise<T> {
        return this.httpClient.invoke(request);
    }

    protected async invokeBlob<T>(request:AxiosRequestConfig):Promise<T> {
        return this.httpClient.invokeBlob(request);
    }
}