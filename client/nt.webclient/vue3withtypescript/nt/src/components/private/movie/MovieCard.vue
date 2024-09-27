<script setup lang="ts">
  import { Movie } from '@/types/MovieTypes';
  interface Props {
    movie: Movie;
  }
  const props = defineProps<Props>();

  console.log('movie props ' + props);
</script>
<template>
  <div class="card p-3 shadow">
    <div class="d-flex align-items-center">
      <div class="ml-3 w-100">
        <h4 class="mb-0 mt-0">{{ movie.title }}</h4>
        <p class="small">({{ movie.language }})</p>
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
    width: 400px;
    border: 10;
    border-radius: 10px;
    background-color: #fff;
  }
</style>
