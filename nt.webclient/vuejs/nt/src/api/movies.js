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

export { getAllMovies, createMovie };
