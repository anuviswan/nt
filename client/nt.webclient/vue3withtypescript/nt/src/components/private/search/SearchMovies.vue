<script setup lang="ts">
  import { ref, watch } from 'vue';
  import { Movie } from '@/types/MovieTypes';
  import MovieCard from '@/components/private/movie/MovieCard.vue';
  import { movieApiService } from '@/apiService/MovieApiService';

  interface Props {
    searchTerm: string;
  }

  const props = defineProps<Props>();

  
  const searchResults = ref<Movie[]>([
    {
      title: 'Source Code',
      language: 'English',
      releaseDate: new Date('2024-12-05'),
      cast: [],
      crew: [],
    },
  ]);

  watch(
    () => props.searchTerm,
    async (newValue) => {
      console.log('Searching for Movies with ' + newValue);

      const results = await movieApiService.SearchMovies(newValue);
      searchResults.value = results;
      console.log(searchResults.value)
    }
    ,
  { immediate: true } 
  );

</script>
<template>
  <div>
    <div v-if="searchResults && searchResults.length > 0">
      <ul class="user-grid">
        <li
          v-for="(movie, index) in searchResults"
          :key="index"
          class="user-list"
        >
          <MovieCard :movie="movie" />
        </li>
      </ul>
    </div>
    <div v-else>
      <i>No movies found !</i>
    </div>
  </div>
</template>
<style scoped>
  .user-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
    gap: 16px;
    padding: 16px;
    grid-auto-rows: min-content; /* Ensure rows take the space they need */
  }
  .user-list {
    list-style-type: none;
    padding: 0;
  }
</style>
