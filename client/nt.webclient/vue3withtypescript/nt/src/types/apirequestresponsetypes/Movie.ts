import { Movie } from '@/types/MovieTypes';
import {  IGraphQlResponseBase } from '@/types/apirequestresponsetypes/Response';

export interface ISearchMoviesResponse extends IGraphQlResponseBase {
  findMovie: Movie[]
  
}
