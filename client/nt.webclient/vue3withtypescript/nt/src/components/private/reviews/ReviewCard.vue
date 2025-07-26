<script lang="ts">
import { Review } from '@/types/ReviewTypes';



interface Props{
    review:Review
}

const props = defineProps<Props>();
</script>
<template>
    <div class="card">
        <div class="card-body">
            <div class="card-title">
                {{ review.title }}
            </div>
            <div class="card-subtitle">
                {{ review.movieTitle }}
            </div>
            <div class="card-body">
                {{ review.content }}
            </div>
        </div>
    </div>
</template>