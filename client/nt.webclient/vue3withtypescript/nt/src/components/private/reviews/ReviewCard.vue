<script setup lang="ts">
  import { Review } from '@/types/ReviewTypes';
  import { defineProps } from 'vue';
  import AvataarCard from '@/components/private/user/AvataarCard.vue';
  interface Props {
    review: Review;
  }

  const props = defineProps<Props>();
</script>
<template>
  <div class="">
    <div class="card shadow-sm mb-4">
      <div class="row g-0">
        <!-- Image Section -->
        <div class="col-md-2">
          <img
            src="https://t4.ftcdn.net/jpg/02/12/52/91/360_F_212529193_YRhcQCaJB9ugv5dFzqK25Uo9Ivm7B9Ca.jpg"
            style="width: 200px"
            class="img-fluid rounded-start h-100 object-fit-cover"
            alt="Movie Poster"
          />
        </div>

        <!-- Content Section -->
        <div class="col-md-10">
          <div class="card-body">
            <h5 class="card-title text-primary">{{ review.title }}</h5>
            <h6 class="card-subtitle mb-2 text-muted">
              {{ review.movieTitle }}
            </h6>

            <p class="card-text mt-3">
              {{ review.content }}
            </p>

            <div class="d-flex justify-content-between align-items-center mt-4">
              <div class="d-flex align-items-center">
                <span class="badge bg-warning text-dark"
                  >⭐ {{ review.rating }}</span
                >
                <div
                  class="d-inline-block rounded-circle border ms-2 bg-primary"
                  style="width: 50px; height: 50px"
                >
                  <AvataarCard
                    :userName="review.displayName"
                    :isReadOnly="true"
                  />
                </div>
              </div>
              <small class="text-muted">Reviewed on Jul 29, 2025</small>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
