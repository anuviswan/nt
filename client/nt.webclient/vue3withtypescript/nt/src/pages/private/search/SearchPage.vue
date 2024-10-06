<template>
  <div class="container-fluid">
    <div class="row align-items-center justify-content-center">
      <div class="col">You searched for '{{ searchTerm }}'</div>
    </div>
    <div class="row">
      <div class="col">
        <div class="card">
          <div class="card-header">Users</div>
          <div class="card-body">
            <SearchUsers v-if="searchTerm"  :searchTerm="searchTerm" />
          </div>
          <div class="card-header">Movies</div>
          <div class="card-body">
            <SearchMovies v-if="searchTerm" :searchTerm="searchTerm" />
          </div>
          <div class="card-header">Reviews</div>

        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
  import { computed,watch } from 'vue';
  import { useRoute } from 'vue-router';
  import SearchUsers from '@/components/private/search/SearchUsers.vue';
  import SearchMovies from '@/components/private/search/SearchMovies.vue';

  const route = useRoute();

  const searchTerm = computed(() => {
    console.log('got search term ' + route.params.searchTerm)
    const param = route.params.searchTerm;
    return Array.isArray(param) ? param[0] : param;
  });

  watch(
  () => route.params.searchTerm,
  (newVal) => {
    console.log('searchTerm updated in parent:', newVal);
    // Update searchTerm in a reactive way
  },
  { immediate: true }
);
</script>

<style scoped></style>
