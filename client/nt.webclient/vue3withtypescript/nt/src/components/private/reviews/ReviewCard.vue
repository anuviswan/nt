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
    return props.review.posterUrl?.trim().length
      ? props.review.posterUrl
      : defaultImage;
  });
</script>
<template>
  <!-- 3-column layout: poster | content | avatar -->
  <div class="row gx-4 gy-0 align-items-start">
    <!-- Poster -->
    <div class="col-auto d-flex align-items-start pe-3">
      <img
        :src="posterUrl"
        alt="Movie Poster"
        class="img-fluid rounded"
        style="width: 150px; height: auto"
      />
    </div>

    <!-- Content -->
    <div class="col d-flex flex-column">
      <div>
        <h5 class="fw-bold mb-1">
          {{ review.title }}
          <small class="fst-italic text-muted ms-1">
            {{ review.language }}
          </small>
        </h5>

        <p class="mb-1">
          <strong>Directed By:</strong> {{ review.director || 'Unknown' }}
        </p>
        <p class="mb-1"><strong>Story:</strong> {{ review.story }}</p>
        <p class="mb-3"><strong>Cast:</strong> {{ review.cast }}</p>

        <blockquote class="fst-italic text-muted border-start ps-3">
          {{ review.content }}
        </blockquote>
      </div>

      <!-- Star Rating at Bottom -->
      <div class="mt-auto pt-2 d-flex align-items-center">
        <div
          class="text-warning me-2"
          style="font-size: 1.2rem; line-height: 1"
        >
          <span v-for="n in 5" :key="n">
            {{ n <= review.rating ? '★' : '☆' }}
          </span>
        </div>
        <small class="text-muted">({{ review.rating }} / 5)</small>
      </div>
    </div>

    <!-- Avatar -->
    <div class="col-auto text-center">
      <div
        class="avatar-top position-absolute"
        style="top: 12px; right: 12px; width: 56px; height: 56px; z-index: 1100"
      >
        <div class="avatar-box">
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
      <div>{{ review.author }}</div>
    </div>
  </div>
</template>
