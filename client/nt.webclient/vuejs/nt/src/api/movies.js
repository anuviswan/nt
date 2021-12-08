import { movies } from "../mock/movies";
import axios from "axios";

const getAllMovies = () => {
  return movies;
};

const createMovie = async (movie) => {
  var response = await axios.post(
    "https://localhost:44353/api/movie/createmovie",
    movie
  );
  console.log(response);
  return response;
};

const searchMovieByTitle = async (searchTerm, maxItems) => {
  const request = {
    searchString: searchTerm,
    criteria: {
      maxItems: maxItems,
    },
  };
  var response = await axios.post(
    "https://localhost:44353/api/Movie/SearchMovieByTitle",
    request
  );

  return response;
};


const getMovie = async (movieId)=>{

  const params = {
    params: {
      movieId: movieId,
    },
  };

  var response = await axios.get("https://localhost:44353/api/Movie/GetMovieMetaInfo",params);
  return response;
}
export { getAllMovies, createMovie, searchMovieByTitle,getMovie };
