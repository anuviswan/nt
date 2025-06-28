import {  IGraphQlResponseBase } from '@/types/apirequestresponsetypes/Response';

export interface ISearchMoviesResponse extends IGraphQlResponseBase {
  findMovie: MovieResponse[]
  
}

export interface IRecentMoviesResponse extends IGraphQlResponseBase{
  recentMovies : MovieResponse[]
}

export interface MovieResponse {
  title: string;
  movieLanguage: string;
  releaseDate: Date;
  cast: PersonResponse[];
  crew: KeyValuePair<string,PersonResponse[]>[];
}

export interface CastResponse{
  Person : PersonResponse[]
}

export interface KeyValuePair<TKey,TValue> {
  key: TKey;
  value: TValue;
}

export interface PersonResponse {
  name: string;
}

