<script setup lang="ts">
import { Person } from '@/types/MovieTypes';
interface Props{
    crewTitle:string;
    crewMembers: Person[]|undefined;
}

const props = defineProps<Props>();

console.log(props)
</script>
<template>
    <div class="row">
       <div class="col"> {{ crewTitle }}</div>
        <div class="col" v-for="(person,index) in crewMembers" :key="index">{{person.name}}</div>
    </div>
</template>