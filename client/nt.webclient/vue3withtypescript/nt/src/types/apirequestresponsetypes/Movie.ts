import { Movie } from '@/types/MovieTypes';
import { IResponseBase } from '@/types/apirequestresponsetypes/Response';

export interface ISearchMoviesResponse extends IResponseBase {
  data:{
    users: Movie[]
  }
  
}
