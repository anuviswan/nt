<template>
  <div>
    <div class="row">
      <div
        v-for="(review, index) in reviewCollection"
        :key="index"
        class="col-md-3 col-6 "
      >
        <ReviewMiniCard v-bind:reviewMetaInfo="review" />
      </div>
    </div>
  </div>
</template>

<script>
import ReviewMiniCard from "../../../components/review/ReviewMiniCard";
import { getReviewsForMovie } from "../../../api/reviews";
export default {
  name: "AllReviews",
  components: {
    ReviewMiniCard,
  },
  data() {
    return {
      reviewCollection: [],
      movieId: "",
    };
  },
  methods: {},
  async created() {
    this.movieId = this.$route.params.movieId;
    console.log("all revies for " + this.movieId);
    const response = await getReviewsForMovie(this.movieId);
    this.reviewCollection = response.data.reviews;
    console.log(this.reviewCollection);
  },
};
</script>

<style></style>
