<template>
<div class="container">
    <div class="row justify-content-end">
        <div class="col-4">
            <div class="bg-light border p-2">
              <div v-if="recentMovies && recentMovies.length > 0">
                <ul class="movie-flex list-unstyled">
                  <li
                    v-for="(movie, index) in recentMovies"
                    :key="index"
                  >
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
</div>
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue';
  import { Movie } from '@/types/MovieTypes';
  import MovieCardMini from "@/components/private/movie/MovieMiniCard.vue"
  import { movieApiService } from '@/apiService/MovieApiService';

  const recentMovies = ref<Movie[]>([
    {
      title: 'Source Code',
      movieLanguage: 'English',
      releaseDate: new Date('2024-12-05'),
      cast: [],
      crew: [],
    },
  ]);

  onMounted(async ()=>{
    await LoadRecentMovies();
  });

  const LoadRecentMovies = async () =>{
    const movies = await movieApiService.GetRecentMovies(10);
    recentMovies.value = movies;
  }

  
</script>

<style scoped>
.movie-flex {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  padding: 16px;
  justify-content: flex-start;
}

.user-list {
  list-style-type: none;
  padding: 0;
  margin: 0;
  flex: 0 1 250px; /* Each card can shrink or grow but has a base width */
}
</style>
