import { IResponseBase } from './Response';

export interface IRecentReviewForUsersRequest {
  userIds: string[];
  count: number;
}

export interface IRecentReviewsForUsersResponse extends IResponseBase {
  reviews: IRecentReviewsForUsersResponseItem[];
}

export interface IRecentReviewsForUsersResponseItem {
  reviewId: string;
  movieId: string;
  content: string;
  rating: number;
  author: string;
}
