import { IResponseBase } from '@/types/apirequestresponsetypes/Response';
export interface IResponseBase {
  hasError: boolean;
  status?: number;
  errors?: Array<string>;
}
