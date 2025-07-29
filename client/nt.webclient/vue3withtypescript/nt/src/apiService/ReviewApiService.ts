import { ApiServiceBase } from './ApiServiceBase';
import {
  IRecentReviewForUsersRequest,
  IRecentReviewsForUsersResponse,
} from '@/types/apirequestresponsetypes/Review';

class ReviewApiService extends ApiServiceBase {
  public async GetRecentReviewsForUsers(
    userIds: string[],
    countOfReviews: number
  ): Promise<IRecentReviewsForUsersResponse> {
    const request: IRecentReviewForUsersRequest = {
      userIds: userIds,
      count: countOfReviews,
    };
    return await this.invoke<IRecentReviewsForUsersResponse>({
      method: 'post',
      url: 'reviews/GetRecentReviewsForUsers',
      data: request,
    });
  }
}

export const reviewApiService = new ReviewApiService();
