import { ApiServiceBase } from './ApiServiceBase';
import { ISearchMoviesResponse } from '@/types/apirequestresponsetypes/Movie';
import { DocumentNode, gql } from '@apollo/client/core';
import  apolloClient  from '@/apolloClient'; 
class MovieApiService extends ApiServiceBase {
  public async SearchMovies(
    searchTerm: string
  ): Promise<ISearchMoviesResponse> {
    const search_movie :DocumentNode = gql`
      query movie($searchTerm: String!) {
        findMovie(searchTerm: $searchTerm) {
          language
          releaseDate
          title,
          cast {
                name
              },
          crew {
            key
            value {
              name
            }
          }
        }
      }
    `;
    const response = await this.queryGraphQl<ISearchMoviesResponse>(search_movie,{ searchTerm})
    return response;
  }
}

export const movieApiService = new MovieApiService();
