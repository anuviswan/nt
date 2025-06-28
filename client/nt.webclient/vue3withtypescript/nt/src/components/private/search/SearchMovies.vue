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
      movieLanguage: 'English',
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
      <ul class="movie-flex">
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
