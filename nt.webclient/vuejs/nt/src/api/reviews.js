import { movieReviews } from "../mock/reviews";
import axios from "axios";

const getAllReviews = () => {
  return movieReviews;
};


const getReviewsForMovie = async (movieId) => {
  const request = {
    movieId : movieId
  };

  const response = await axios.post("https://localhost:44353/api/Review/GetAllReviews",request);
  return response;
}


const createReview = async (movieid,title,description,rating) => {

  const review = {
    movieid : movieid,
    title:title,
    description:description,
    rating:rating
  }
  const response = await axios.post("https://localhost:44353/api/Review/CreateReview",review);
  return response;
}
export { getAllReviews,createReview,getReviewsForMovie };
