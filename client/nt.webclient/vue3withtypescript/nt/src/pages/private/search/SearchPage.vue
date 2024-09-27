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
            <SearchUsers :searchTerm="searchTerm" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { computed } from "vue";
import { useRoute } from "vue-router";
import SearchUsers from "@/components/private/search/SearchUsers.vue";

const route = useRoute();

const searchTerm = computed(() => {
  const param = route.params.searchTerm;
  return Array.isArray(param) ? param[0] : param;
});
</script>

<style scoped></style>
