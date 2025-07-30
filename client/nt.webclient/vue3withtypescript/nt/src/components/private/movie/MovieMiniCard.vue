<script setup lang="ts">
  import { Movie, Person } from '@/types/MovieTypes';
  import CrewCard from './CrewCard.vue';
  import { defineProps } from 'vue';

  interface Props {
    movie: Movie;
  }
  const props = defineProps<Props>();

  function findCrewMembers(
    currentMovie: Movie,
    key: string
  ): Person[] | undefined {
    if (currentMovie.crew) {
      return currentMovie.crew.find((crewMember) => crewMember.key === key)
        ?.value;
    } else return undefined;
  }

  function formatDate(date: string | Date): string {
    const parsedDate = typeof date === 'string' ? new Date(date) : date;

    return parsedDate.toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'long',
      day: 'numeric',
    });
  }
</script>
<template>
  <div class="card p-3 shadow">
    <div class="d-flex align-items-center">
      <div class="ml-3 w-100">
        <h4 class="mb-0 mt-0">{{ movie.title }}</h4>
        <p class="small">({{ movie.movieLanguage }})</p>
        <p class="small">Released on : {{ formatDate(movie.releaseDate) }}</p>
      </div>
    </div>

    <!-- Director Section Below Language -->
    <div class="row mt-3">
      <div class="col">
        <div class="row" v-if="findCrewMembers(movie, 'Director')">
          <div class="col">
            <CrewCard
              crew-title="Director"
              :crew-members="findCrewMembers(movie, 'Director')"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
  .small {
    font-size: 12px;
    font-style: italic;
  }
  .card {
    width: 300px;
    border: 10;
    border-radius: 10px;
    background-color: #fff;
  }
</style>
