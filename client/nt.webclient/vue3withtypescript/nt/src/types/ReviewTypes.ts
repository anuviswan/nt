export interface Review {
  reviewId: string;
  title: string;
  content: string;
  movieId: string;
  rating: number;
  userName: string;
  posterUrl: string;
  language: string;
  upvotedBy?: string[];
  downvotedBy?: string[];
}
