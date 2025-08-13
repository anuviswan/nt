<template>
  <div class="container-fluid">
    <div class="row">
      <!-- Recent Reviews -->
      <div class="col-10">
        <div class="row g-3">
          <div
            class="col-md-6"
            v-for="(review, index) in recentReviews"
            :key="index"
          >
            <ReviewCard :review="review" />
          </div>
        </div>
      </div>

      <!-- Recent Movie Sidebar -->
      <div class="col-2">
        <div v-if="recentMovies && recentMovies.length > 0">
          <ul class="list-unstyled">
            <li v-for="(movie, index) in recentMovies" :key="index">
              <MovieCardMini :movie="movie" />
            </li>
          </ul>
        </div>
        <div v-else>
          <i>No movies found !</i>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { onMounted, ref } from 'vue';
  import { Movie } from '@/types/MovieTypes';
  import MovieCardMini from '@/components/private/movie/MovieMiniCard.vue';
  import { movieApiService } from '@/apiService/MovieApiService';
  import { reviewApiService } from '@/apiService/ReviewApiService';
  import { Review } from '@/types/ReviewTypes';
  import ReviewCard from '@/components/private/reviews/ReviewCard.vue';

  const recentMovies = ref<Movie[]>([
    {
      title: 'Source Code',
      movieLanguage: 'English',
      releaseDate: new Date('2024-12-05'),
      cast: [],
      crew: [],
    },
  ]);

  const recentReviews = ref<Review[]>([]);

  onMounted(async () => {
    await LoadRecentMovies();
    await LoadTimeLine();
  });

  const LoadRecentMovies = async (): Promise<void> => {
    recentMovies.value = await movieApiService.GetRecentMovies(10);
  };

  const LoadTimeLine = async (): Promise<void> => {
    const reviews = await reviewApiService.GetRecentReviewsForUsers(
      ['jia.anu'],
      10
    );

    recentReviews.value = reviews.reviews.map((review) => ({
      reviewId: review.reviewId,
      title: review.title,
      content: review.content,
      movieId: review.movieId,
      movieTitle: 'Demo Title',
      userName: review.userName,
      rating: review.rating,
      displayName: 'Demo User',
      posterUrl: '',
      director: '',
      language: review.movieLanguage,
    }));

    recentReviews.value.map(async (review) => {
      const movieInfo = await GetMovieInfo(review.movieId);
      review.movieTitle = movieInfo.title;
    });
    console.log(reviews);
  };

  const GetMovieInfo = async (movieId: string): Promise<Movie> => {
    return await movieApiService.GetMovieById(movieId);
  };
</script>

<style scoped></style>
