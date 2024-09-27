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

    const { result, loading, error } = useQuery(search_movie);
    console.log(result);
    throw 'Not Implemented Exception';
  }
}

export const movieApiService = new MovieApiService();
