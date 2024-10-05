import { ApiServiceBase } from './ApiServiceBase';
import { ISearchMoviesResponse,MovieResponse } from '@/types/apirequestresponsetypes/Movie';
import { Movie } from "@/types/MovieTypes"
import { DocumentNode, gql } from '@apollo/client/core';
import { plainToClass } from 'class-transformer';

class MovieApiService extends ApiServiceBase {
  public async SearchMovies(
    searchTerm: string
  ): Promise<Movie[]> {

    const search_movie: DocumentNode = gql`
    query findMovieQuery($searchTerm:String!) {
    findMovie(searchTerm: $searchTerm) {
      language
      releaseDate
      title
      cast(first:5) {
        edges {node{
          name
        }}
        
      }
      crew{
          key
          value{
            name
          }
          }
        }
    }
  
    `
    const response = await this.queryGraphQl<ISearchMoviesResponse>(search_movie,{ searchTerm})
    const movies = response.findMovie.map(movieResponse => (ConvertToMovieDto(movieResponse)));
    return movies;
  }
}

function ConvertToMovieDto(movieResponse:MovieResponse):Movie{
  const movie = {
    ...movieResponse,
    cast: movieResponse.cast.edges.map(edge=>({name:edge.node.name})),
    crew: movieResponse.crew.map(kvp=>({
      key : kvp.key,
      value: kvp.value.map((p)=>({ name: p.name}))
    }))
  };

  return  movie;
}
export const movieApiService = new MovieApiService();
