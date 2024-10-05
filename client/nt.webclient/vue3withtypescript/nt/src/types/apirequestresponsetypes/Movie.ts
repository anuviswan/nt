import {  IGraphQlResponseBase } from '@/types/apirequestresponsetypes/Response';

export interface ISearchMoviesResponse extends IGraphQlResponseBase {
  findMovie: MovieResponse[]
  
}

export interface MovieResponse {
  title: string;
  language: string;
  releaseDate: Date;
  cast: CastResponse;
  crew: KeyValuePair<string,PersonResponse[]>[];
}

export interface CastResponse{
  edges : PersonEdgeResponse[]
}
export interface PersonEdgeResponse{
  node: PersonResponse
}

export interface KeyValuePair<TKey,TValue> {
  key: TKey;
  value: TValue;
}

export interface PersonResponse {
  name: string;
}

