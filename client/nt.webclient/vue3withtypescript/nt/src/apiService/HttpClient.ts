import { IResponseBase, IGraphQlResponseBase } from "@/types/apirequestresponsetypes/Response";
import axios, {AxiosInstance, AxiosRequestConfig} from "axios";
import {useUserStore} from "@/stores/userStore";
import { DocumentNode, gql } from '@apollo/client/core';
import  apolloClient  from '@/apolloClient'; 

class HttpClient{

    private axiosInstance : AxiosInstance;
    
    constructor(){
        const headers = {
            "Access-Control-Allow-Origin": "*",
            "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
            "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
            "Content-Type": "application/json", // this shows the expected content type
          };

          console.log('base URL' + import.meta.env.VITE_APP_API_URL)
        this.axiosInstance = axios.create({baseURL: import.meta.env.VITE_APP_API_URL, headers:headers});
        
        this.axiosInstance.interceptors.request.use(function (config) 
        {
            const userStoreInstance = useUserStore();
            if(userStoreInstance.Token){
                console.log("Submitting with token " + userStoreInstance.Token)
                config.headers.Authorization  = `Bearer ${userStoreInstance.Token}`; 
            }
            else{
                console.log("Token not available")
            }
            return config;
        });
    }

    public async invoke<T extends IResponseBase>(config:AxiosRequestConfig):Promise<T> {

        try{
            const response =await this.axiosInstance.request<T>(config); 
            console.log(response.data)
            return response.data;
        }catch(error : unknown){

            if(axios.isAxiosError(error)){
                return <T>{
                    status : error.response?.status,
                    hasError : true,
                    errors : error.response?.data.errors
                } 
            }
            else{
                console.log("Some other error ?? " + error);
            }
        } 
        return <T>{};
    }
    public async getBlob<T>(config:AxiosRequestConfig):Promise<T|null>
    {
        try{
            const response =await this.axiosInstance.request<T>(config); 
            console.log(response.data)
            return response.data;
        }catch(error : unknown){

            if(axios.isAxiosError(error)){
                return null;
            }
            else{
                console.log("Some other error ?? " + error);
            }
        } 
        return <T>{};
    }

    public async queryGraphQl<T extends IGraphQlResponseBase>(query:DocumentNode,variable:object):Promise<T>{

        try {

            console.log("GraphQL Query:", query.loc?.source.body); // Shows full query string
console.log("Variables:", JSON.stringify(variable, null, 2));

            const result = await apolloClient.query<T>({ query, variables: variable });
            console.log(result.data);
            return result.data;
          } catch (err) {
            console.error("GraphQL request failed:", err);
            throw err;
          }
    }
}

export default HttpClient;