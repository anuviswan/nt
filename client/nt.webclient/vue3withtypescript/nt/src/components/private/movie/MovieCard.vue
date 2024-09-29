<script setup lang="ts">
  import { Movie, Person } from '@/types/MovieTypes';
  import CrewsCard from './CrewsCard.vue';
  interface Props {
    movie: Movie;
  }
  const props = defineProps<Props>();


  function findCrewMembers(currentMovie:Movie,key:string):Person[]|undefined{
    if(currentMovie.crew)
  {
    console.log(currentMovie)
    console.log(currentMovie.crew)
    return currentMovie.crew.find(crewMember => crewMember.key === key)?.value;
  }
  else
  return undefined;
  }


</script>
<template>
 <div class="card p-3 shadow">
  <div class="d-flex align-items-center">
    <div class="ml-3 w-100">
      <h4 class="mb-0 mt-0">{{ movie.title }}</h4>
      <p class="small">({{ movie.language }})</p>
    </div>
  </div>

  <!-- Director Section Below Language -->
  <div class="row mt-3">
  <div class="col">


    <!-- Include the CrewsCard -->
    <div class="row" v-if="findCrewMembers(movie, 'Director')">
      <div class="col">
        <CrewsCard crew-title="Director" :crew-members="findCrewMembers(movie, 'Director')"/>
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
    width: 400px;
    border: 10;
    border-radius: 10px;
    background-color: #fff;
  }
</style>
