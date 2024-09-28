import { ApiServiceBase } from './ApiServiceBase';
import { ISearchMoviesResponse } from '@/types/apirequestresponsetypes/Movie';
import { gql } from '@apollo/client/core';
import  apolloClient  from '@/apolloClient'; 
class MovieApiService extends ApiServiceBase {
  public async SearchMovies(
    searchTerm: string
  ): Promise<ISearchMoviesResponse> {
    const search_movie = gql`
      query movie($searchTerm: String!) {
        findMovie(searchTerm: $searchTerm) {
          language
          releaseDate
          title
        }
      }
    `;

    console.log('submitting gql query')
    const response = await apolloClient.query<ISearchMoviesResponse>({
      query: search_movie,
      variables: { searchTerm },  // Pass the search term as a variable
    });
    console.log('recieved result from movie graphql')
    console.log(response.data);
    return response.data;
  }
}

export const movieApiService = new MovieApiService();
