import { ApiServiceBase } from './ApiServiceBase';
import { ISearchMoviesResponse } from '@/types/apirequestresponsetypes/Movie';
import { gql } from '@apollo/client/core';
import { useQuery } from '@vue/apollo-composable';
class MovieApiService extends ApiServiceBase {
  public async SearchMovies(
    searchTerm: string
  ): Promise<ISearchMoviesResponse> {
    const search_movie = gql`
      query movie {
        findMovie(searchTerm: "yo") {
          language
          releaseDate
          title
        }
      }
    `;

    console.log('submitting gql query')
    const { result, loading, error } = useQuery<ISearchMoviesResponse>(search_movie);
    console.log('recieved result from movie graphql')
    console.log(result);
    return result;
  }
}

export const movieApiService = new MovieApiService();
