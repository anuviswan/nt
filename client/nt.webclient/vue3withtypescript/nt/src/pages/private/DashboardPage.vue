<template>
<div class="container-fluid">
    <div class="row">
      <div class="col"></div>
      
    </div>
</div>

<!-- Recent Movie Sidebar -->
<div class="position-fixed top-0 right-0 h-100 bg-light border p-2">
              <div v-if="recentMovies && recentMovies.length > 0">
                <ul class="list-unstyled">
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


</style>
