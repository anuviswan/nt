<script lang="ts" setup>
  import { Review } from '@/types/ReviewTypes';
  import { defineProps, computed } from 'vue';
  import AvataarCard from '@/components/private/user/AvataarCard.vue';
  import defaultImage from '@/assets/DefaultMoviePoster.png';

  interface Props {
    review: Review;
  }

  const props = defineProps<Props>();

  const posterUrl = computed(() => {
    return props.review.posterUrl ?? defaultImage;
  });
</script>
<template>
  <div class="card review-card shadow-sm mb-4 p-3 position-relative">
    <!-- === Avatar: MUST be a direct child of .card (not inside .row/.col) === -->
    <div
      class="avatar-top position-absolute"
      style="top: 12px; right: 12px; width: 56px; height: 56px; z-index: 1100"
    >
      <div class="avatar-box">
        <!-- constrain the inner component so it fits the box -->
        <AvataarCard
          :isReadOnly="true"
          :show-user-name="false"
          :showDisplayName="true"
          :userName="review.userName"
          style="
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
            display: block;
          "
        />
      </div>
    </div>

    <div class="row g-0">
      <!-- Poster -->
      <div class="col-md-2 d-flex align-items-start">
        <img
          :src="posterUrl"
          alt="Movie Poster"
          class="img-fluid rounded"
          style="max-width: 150px"
        />
      </div>

      <!-- Content -->
      <div class="col-md-10">
        <h5 class="fw-bold mb-1">
          {{ review.title }}
          <small class="fst-italic text-muted ms-1">{{
            review.language
          }}</small>
        </h5>

        <p class="mb-1"><strong>Directed By:</strong> {{ review.director }}</p>
        <p class="mb-1"><strong>Story:</strong> {{ review.story }}</p>
        <p class="mb-3"><strong>Cast:</strong> {{ review.cast }}</p>

        <blockquote class="fst-italic text-muted border-start ps-3">
          {{ review.content }}
        </blockquote>
      </div>
    </div>
  </div>
</template>
