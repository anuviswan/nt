<template>
  <div>
    <div class="card">
      <div class="row no-gutters">
        <div class="col-sm-5 bg-secondary">
          <div class="card">
            <div class="card-body">
              <div class="d-flex flex-column align-items-center text-center">
                <img
                  src="https://bootdey.com/img/Content/avatar/avatar7.png"
                  alt="Admin"
                  class="rounded-circle"
                  width="150"
                />
                <div class="mt-3">
                  <h4>{{ this.user.displayName }}</h4>
                  <p class="text-secondary mb-1">
                    {{ this.user.followers }} Followers
                  </p>
                  <p class="text-muted font-size-sm">
                    User Rating: 5
                  </p>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="col-sm-7">
          <div class="card-body">
            <ul class="list-group list-group-flush">
              <li class="list-group-item">
                <h5 class="card-title">{{ this.reviewTitle }}</h5>
                <Star-rating
                  v-bind:inline="true"
                  v-bind:star-size="15"
                  v-bind:read-only="true"
                  v-bind:fixed-points="1"
                  v-bind:increment="0.5"
                  v-model="movieRating"
                  v-bind:animate="true"
                  v-bind:show-rating="false"
                  text-class="invisible"
                />
              </li>
              <li class="list-group-item">
                <b class="card-title">{{ this.movieTitle }}</b>
              </li>
            </ul>

            <p class="card-text">
              {{ this.review }}
            </p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import StarRating from "vue-star-rating";
export default {
  name: "ReviewMiniCard",
  components: {
    StarRating,
  },
  props: {
    reviewMetaInfo: {
      required: true,
      type: Object,
    },
  },
  data() {
    return {
      reviewTitle: "",
      movieTitle: "",
      review: "",
      movieRating: 5,
      user: {},
    };
  },
  created() {
    console.log(this.reviewMetaInfo);
    this.reviewTitle = this.reviewMetaInfo.title;
    this.movieTitle = this.reviewMetaInfo.movieTitle;
    this.review = this.reviewMetaInfo.description;
    this.movieRating = this.reviewMetaInfo.rating;
    this.user.displayName = this.reviewMetaInfo.authorUserName;
    this.user.rating = this.reviewMetaInfo.rating;
    this.user.followers = this.reviewMetaInfo.authorFollowers;
  },
};
</script>

<style scoped>
.card-horizontal {
  display: flex;
  flex: 1 1 auto;
}

.card {
  max-width: 500px;
}

.fill-height-or-more > div {
  flex: 1;

  display: flex;
  justify-content: center;
  flex-direction: column;
}
</style>
