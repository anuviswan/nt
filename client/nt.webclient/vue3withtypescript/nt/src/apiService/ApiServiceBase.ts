import {
  IGraphQlResponseBase,
  IResponseBase,
} from '@/types/apirequestresponsetypes/Response';
import { AxiosRequestConfig } from 'axios';
import HttpClient from './HttpClient';
import { DocumentNode } from '@apollo/client/core';

export abstract class ApiServiceBase {
  private httpClient: HttpClient;

  constructor() {
    this.httpClient = new HttpClient();
  }

  protected async invoke<T extends IResponseBase>(
    request: AxiosRequestConfig
  ): Promise<T> {
    return this.httpClient.invoke(request);
  }

  protected async getBlob<T>(request: AxiosRequestConfig): Promise<T | null> {
    return this.httpClient.getBlob(request);
  }

  protected async queryGraphQl<T extends IGraphQlResponseBase>(
    query: DocumentNode,
    variable: object
  ): Promise<T> {
    return this.httpClient.queryGraphQl<T>(query, variable);
  }
}
