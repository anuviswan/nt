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
import { getRecentReviews } from "../../api/reviews";
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
  async created() {
    const response = await getRecentReviews();
    this.reviewCollection = response.data.reviews;
    console.log(this.reviewCollection);
  },
};
</script>

<style></style>
