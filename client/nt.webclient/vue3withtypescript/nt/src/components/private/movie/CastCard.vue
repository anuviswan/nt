<script setup lang="ts">
import { Person } from '@/types/MovieTypes';
interface Props{
    cast: Person[]|undefined;
}

const props = defineProps<Props>();

console.log(props)
</script>
<template>
    <div class="row">
      <div class="col"><b>Cast</b></div>
      <div class="col">
        <div class="row" v-for="(person, index) in cast" :key="index">
          <div class="col">{{ person.name }}</div>
        </div>
      </div>
    </div>
  </template>
  