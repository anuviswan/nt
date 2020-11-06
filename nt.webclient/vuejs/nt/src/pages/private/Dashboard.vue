<template>
  <div>
    <div class="row">
      <div
        v-for="(review, index) in reviewCollection"
        :key="index"
        class="col-md-3 col-6 my-1"
      >
        <ReviewMiniCard v-bind:reviewMetaInfo="review" />
      </div>
    </div>
  </div>
</template>

<script>
import ReviewMiniCard from "../../components/review/ReviewMiniCard";
export default {
  name: "Dashboard",
  components: {
    ReviewMiniCard,
  },
  data() {
    return {
      reviewCollection: [
        {
          review: {
            reviewTitle: "Awesome action !!",
            movieTitle: "Matrix",
            review:
              "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type ",
            upvotes: 3,
            downvotes: 6,
          },
          user: {
            userName: "Anu Viswan",
            rating: 5,
            followers: 345,
          },
        },
        {
          review: {
            reviewTitle: "Average action !!",
            movieTitle: "Matrix",
            review:
              "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type ",
            upvotes: 3,
            downvotes: 6,
          },
          user: {
            userName: "Manu Viswan",
            rating: 5,
            followers: 345,
          },
        },
        {
          review: {
            reviewTitle: "Super",
            movieTitle: "Lion King",
            review:
              "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type ",
            upvotes: 3,
            downvotes: 6,
          },
          user: {
            userName: "Jia Anu",
            rating: 5,
            followers: 345,
          },
        },
        {
          review: {
            reviewTitle: "aha....",
            movieTitle: "Boss Baby",
            review:
              "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type ",
            upvotes: 3,
            downvotes: 6,
          },
          user: {
            userName: "Naina Anu",
            rating: 5,
            followers: 345,
          },
        },
        {
          review: {
            reviewTitle: "aha....",
            movieTitle: "Jumbo",
            review:
              "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type ",
            upvotes: 3,
            downvotes: 6,
          },
          user: {
            userName: "Sreena Anu",
            rating: 5,
            followers: 345,
          },
        },
      ],
    };
  },
  methods: {},
};
</script>

<style></style>
