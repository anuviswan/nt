<script setup lang="ts">
import { Person } from '@/types/MovieTypes';


interface Props{
    crewTitle:string,
    crewMembers: Person[]|undefined
}

const props = withDefaults(defineProps<Props>(),{
    crewTitle : 'Director',
    crewMemers: ['ac','def']
});

console.log(props)
</script>
<template>
    <div>
        <div>asdasd</div>
        {{ crewTitle }}
<div class="col" v-for="(person,index) in crewMembers" :key="index">{{person.name}}</div>
</div>
</template>