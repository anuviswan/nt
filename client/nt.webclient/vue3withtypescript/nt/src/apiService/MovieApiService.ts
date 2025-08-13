import { ApiServiceBase } from './ApiServiceBase';
import {
  IGetMovieInfoResponse,
  IRecentMoviesResponse,
  ISearchMoviesResponse,
  MovieResponse,
} from '@/types/apirequestresponsetypes/Movie';
import { Movie } from '@/types/MovieTypes';
import { DocumentNode, gql } from '@apollo/client/core';

class MovieApiService extends ApiServiceBase {
  public async SearchMovies(searchTerm: string): Promise<Movie[]> {
    const search_movie: DocumentNode = gql`
      query findMovieQuery($searchTerm: String!) {
        findMovie(searchTerm: $searchTerm) {
          movieLanguage
          releaseDate
          title
          cast {
            name
          }
          crew {
            key
            value {
              name
            }
          }
        }
      }
    `;

    console.log('ready to search for movies');
    const response = await this.queryGraphQl<ISearchMoviesResponse>(
      search_movie,
      { searchTerm }
    );
    const movies = response.findMovie.map((movieResponse) =>
      ConvertToMovieDto(movieResponse)
    );
    return movies;
  }

  public async GetRecentMovies(count: number): Promise<Movie[]> {
    const recent_movie: DocumentNode = gql`
      query recentMovieQuery($count: Int) {
        recentMovies(count: $count) {
          title
          movieLanguage
          releaseDate
          cast {
            name
          }
        }
      }
    `;

    const response = await this.queryGraphQl<IRecentMoviesResponse>(
      recent_movie,
      { count }
    );
    const movies = response.recentMovies.map((movieResponse) =>
      ConvertToMovieDto(movieResponse)
    );
    return movies;
  }

  public async GetMovieById(id: string): Promise<Movie> {
    console.log('query recent movie');
    const movieById: DocumentNode = gql`
      query movieByIdQuery($id: String) {
        movieById(id: $id) {
          id
          title
          description
          movieLanguage
          releaseDate
          cast {
            name
          }
          crew {
            key
            value {
              name
            }
          }
        }
      }
    `;

    const response = await this.queryGraphQl<IGetMovieInfoResponse>(movieById, {
      id,
    });

    console.log(response);
    return ConvertToMovieDto(response.movieById);
  }
}

function ConvertToMovieDto(movieResponse: MovieResponse): Movie {
  const movie = {
    ...movieResponse,
    cast: movieResponse.cast?.map((m) => ({
      name: m.name,
    })),
    crew:
      movieResponse.crew?.map((kvp) => ({
        key: kvp.key,
        value: kvp.value?.map((p) => ({ name: p.name })),
      })) ?? [],
  };

  return movie;
}
export const movieApiService = new MovieApiService();
