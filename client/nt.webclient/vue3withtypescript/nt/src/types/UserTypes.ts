import {Review} from '@/types/ReviewTypes'
export interface User{
    userName : string,
    displayName? : string,
    bio? : string,
    countOfReviews?:0,
    countOfFollowers?:0,
    countOfFollowedUsers?:0,
    reviews?:Review,
    Uprated?:0
    Downrated?:0
}