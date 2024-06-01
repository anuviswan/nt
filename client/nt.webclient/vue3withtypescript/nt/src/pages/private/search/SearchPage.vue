<template>
    <div class="container-fluid">
        <div class="row align-items-center justify-content-center">
            <div class="col">
                You searched for '{{ searchTerm }}'
            </div>
        </div>
        <div class="row">
            <div class="col">
                <nav>
                    <div class="nav nav-tabs" id="nav-tab" role="tablist">
                        <button class="nav-link active" id="nav-users-tab" data-bs-toggle="tab"
                            data-bs-target="#nav-users" type="button" role="tab" aria-controls="nav-users"
                            aria-selected="true">Users</button>
                        <button class="nav-link" id="nav-movies-tab" data-bs-toggle="tab" data-bs-target="#nav-movies"
                            type="button" role="tab" aria-controls="nav-movies" aria-selected="false">Movies</button>
                    </div>
                </nav>
                <div class="tab-content" id="nav-tabContent">
                    <div class="tab-pane fade show active" id="nav-users" role="tabpanel" aria-labelledby="nav-users-tab">
                       sdfsdfsdf</div>
                    <div class="tab-pane fade" id="nav-movies" role="tabpanel" aria-labelledby="nav-movies-tab">...
                        1111111
                    </div>
                </div>

            </div>
        </div>
    </div>
</template>
<script setup lang="ts">
import { computed } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();

const searchTerm = computed(() => {
    console.log(route.params)
    return route.params.searchTerm;
})


</script>

<style scoped></style>