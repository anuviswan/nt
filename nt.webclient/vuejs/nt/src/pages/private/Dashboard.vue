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
import ReviewMiniCard from "../../components/review/ReviewMiniCard";
import { getAllReviews } from "../../api/reviews";
export default {
  name: "Dashboard",
  components: {
    ReviewMiniCard,
  },
  data() {
    return {
      reviewCollection: [],
    };
  },
  methods: {},
  created() {
    this.reviewCollection = getAllReviews();
  },
};
</script>

<style></style>
