<template>
  <div class="container-fluid">
    <div class="row">
      <div v-for="(review, index) in recentReviews" :key="index">
        <div class="col">
          <ReviewCard :review="review"></ReviewCard>
        </div>
      </div>
    </div>
  </div>

  <!-- Recent Movie Sidebar -->
  <div class="position-fixed top-0 right-0 h-100 bg-light border p-2">
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
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue';
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

  const LoadRecentMovies = async () => {
    const movies = await movieApiService.GetRecentMovies(10);
    recentMovies.value = movies;
  };

  const LoadTimeLine = async () => {
    const reviews = await reviewApiService.GetRecentReviewsForUsers({
      userIds: ['jia.anu'],
      count: 10,
    });

    recentReviews.value = reviews.reviews.map((review) => ({
      reviewId: review.reviewId,
      title: review.title,
      content: review.content,
      movieId: review.movieId,
      movieTitle: ' ',
      userName: review.author,
      rating: review.rating,
      displayName: ' ',
    }));

    console.log(reviews);
  };
</script>

<style scoped></style>
