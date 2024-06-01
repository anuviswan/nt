<template>
    <div class="container-fluid">
        <div class="row align-items-center justify-content-center">
            <div class="col">
                You searched for '{{ searchTerm }}'
            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { defineProps, withDefaults, computed } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const searchTerm = computed(()=>{
    console.log(route.params)
    return route.params.searchTerm; 
})


</script>

<style scoped></style>