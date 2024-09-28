import { Movie } from '@/types/MovieTypes';
import {  IGraphQlType } from '@/types/apirequestresponsetypes/Response';

export interface ISearchMoviesResponse extends IGraphQlType {
  findMovie: Movie[]
  
}
