import { ApiServiceBase } from './ApiServiceBase';
import {
  IRecentReviewForUsersRequest,
  IRecentReviewsForUsersResponse,
} from '@/types/apirequestresponsetypes/Review';

class ReviewApiService extends ApiServiceBase {
  public async GetRecentReviewsForUsers(
    userIds: IRecentReviewForUsersRequest
  ): Promise<IRecentReviewsForUsersResponse> {
    return await this.invoke<IRecentReviewsForUsersResponse>({
      method: 'post',
      url: 'api/user/createuser',
      data: userIds,
    });
  }
}

export const reviewApiService = new ReviewApiService();
