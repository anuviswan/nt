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
import { getMovie } from "../../../api/movies";
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
    const movieInfo = await getMovie(this.movieId);
    console.log("all revies for " + this.movieId);
    const response = await getReviewsForMovie(this.movieId);
    this.reviewCollection = response.data.reviews;
    console.log(movieInfo);
    this.reviewCollection.map((item) => {
      item.movieId = this.movieId;
      item.movieTitle = movieInfo.data.title;
    });
    console.log(this.reviewCollection);
  },
};
</script>

<style></style>
