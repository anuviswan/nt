<script lang="ts" setup>
  import { Review } from '@/types/ReviewTypes';
  import { defineProps, computed, onMounted, ref } from 'vue';
  import AvataarCard from '@/components/private/user/AvataarCard.vue';
  import defaultImage from '@/assets/DefaultMoviePoster.png';
  import { Movie } from '@/types/MovieTypes';
  import { movieApiService } from '@/apiService/MovieApiService';
  import Votes from '@/components/private/reviews/Votes.vue';
  import { useUserStore } from '@/stores/userStore';

  interface Props {
    review: Review;
  }

  const props = defineProps<Props>();
  const userStore = useUserStore();

  const movie = ref<Movie>({
    title: 'Source Code',
    movieLanguage: 'English',
    releaseDate: new Date('2024-12-05'),
    cast: [],
    crew: [],
  });
  const posterUrl = computed(() => {
    return props.review.posterUrl?.trim().length
      ? props.review.posterUrl
      : defaultImage;
  });

  onMounted(() => {
    console.log('Mounted');
    GetMovieInfo(props.review.movieId).then((m) => {
      console.log('found' + m);
      movie.value = m;
    });
  });

  const GetMovieInfo = async (movieId: string): Promise<Movie> => {
    return await movieApiService.GetMovieById(movieId);
  };

  const HasVoted = computed(() => {
    return userStore.loggedInUser.userName == props.review.userName;
  });
</script>
<template>
  <!-- 3-column layout: poster | content | avatar -->
  <div
    class="row gx-4 gy-0 align-items-start border m-1 shadow rounded-2 p-3 h-100"
  >
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
    <div class="col d-flex flex-column align-items-start">
      <div class="text-start w-100">
        <h5 class="fw-bold mb-1">
          {{ review.title }}
        </h5>
        <h5 class="fw-bold mb-1">
          {{ movie.title }}
          <small class="fst-italic text-muted ms-1">
            {{ movie.movieLanguage }}
          </small>
        </h5>
        <blockquote class="fst-italic text-muted border-start ps-3">
          {{ review.content }}
        </blockquote>
      </div>

      <div
        class="mt-auto pt-2 d-flex align-items-center w-100 justify-content-between"
      >
        <!-- Rating -->
        <div class="d-flex align-items-center">
          <div
            class="text-warning me-2"
            style="font-size: 1.2rem; line-height: 1"
          >
            <span v-for="n in 5" :key="n">{{
              n <= review.rating ? '★' : '☆'
            }}</span>
          </div>
          <small class="text-muted">({{ review.rating }} / 5)</small>
        </div>

        <!-- Votes -->

        <div class="d-flex gap-5">
          <Votes
            Alignment="Right"
            :IsUpvote="true"
            :Count="review.upvotedBy?.length ?? 0"
            :IsSelected="HasVoted"
            :Text="Upvoted"
          />
          <Votes
            Alignment="Left"
            :IsUpvote="false"
            :Count="review.downvotedBy?.length ?? 0"
            :IsSelected="HasVoted"
            Text="Downvoted"
          />
        </div>
      </div>
    </div>

    <!-- Avatar -->
    <div class="col-auto text-center h-100">
      <div style="width: 56px; height: 56px">
        <AvataarCard
          :isReadOnly="true"
          :show-user-name="false"
          :showDisplayName="false"
          :userName="review.userName"
          :show-initials="true"
        />
      </div>
    </div>
  </div>
</template>
