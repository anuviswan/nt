import { IResponseBase } from './Response';

export interface IRecentReviewForUsersRequest {
  userIds: string[];
  count: number;
}

export interface IRecentReviewsForUsersResponse extends IResponseBase {
  reviews: IRecentReviewsForUsersResponseItem[];
}

export interface IRecentReviewsForUsersResponseItem {
  title: string;
  reviewId: string;
  movieId: string;
  content: string;
  rating: number;
  author: string;
}
