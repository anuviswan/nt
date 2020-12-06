<template>
  <div class="row">
    <div class="col-3">
      <MovieItem v-bind:movie="movie" :key="movie.title" />
    </div>
    <div class="col-9 container-fluid">
      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header font-weight-bold text-uppercase">
              Add Review
            </div>
            <div class="card-body">
              <form
                class="form-horizontal needs-validation"
                v-on:submit="onSubmit"
              >
                <div class="form-group">
                  <label for="reviewTitle">Review Title</label>
                  <input
                    type="text"
                    placeholder="Review Title"
                    name="reviewTitle"
                    v-model="reviewTitle"
                    v-bind:class="
                      hasError('reviewTitle')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                    "
                  />
                </div>
                <div class="form-group">
                  <label for="reviewDescription">Description</label>
                  <input
                    type="text"
                    v-model="reviewDescription"
                    name="reviewDescription"
                    placeholder="Write your review here"
                    v-bind:class="
                      hasError('reviewDescription')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                    "
                  />
                </div>

                <div class="form-group">
                  <Star-rating
                    star-size="25"
                    animate="true"
                    show-rating="false"
                    text-class="invisible"
                  />
                </div>

                <div class="form-group">
                  <input
                    type="submit"
                    value="Save Changes"
                    class="btn-primary"
                  />
                </div>
                <div v-bind:class="showServerMessage()">
                  <ul>
                    <li v-for="(error, index) in serverMessage" :key="index">
                      <small>{{ error }}</small>
                    </li>
                  </ul>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { getMovie } from "../../../api/movies";
import { createReview } from "../../../api/reviews";
import MovieItem from "../../../components/movie/MovieItem";
import StarRating from "vue-star-rating";

export default {
  name: "CreateReview",
  components: { MovieItem, StarRating },
  data() {
    return {
      movie: {
        id: "",
        title: "",
        releaseDate: new Date(),
        rating: 4,
        language: "",
        plotSummary: "Sample plot summary",
      },
      reviewTitle: "",
      reviewDescription: "",
      errors: [],
      serverMessage: [],
      hasServerError: false,
      currentRating: 4,
    };
  },
  methods: {
    async onSubmit(e) {
      e.preventDefault();

      if (!this.validateForm()) {
        console.log(this.errors);
        console.log("Form is INVALID");
        return;
      }

      const response = await createReview(
        this.movie.id,
        this.reviewTitle,
        this.reviewDescription,
        this.currentRating
      );

      if (response.hasError) {
        this.hasServerError = true;
        this.serverMessage = response.error;
        return;
      }
      this.serverMessage.push("Review added successfully");
    },
    hasError(key) {
      return this.errors.indexOf(key) != -1;
    },
    showServerMessage() {
      if (!this.serverMessage.length) {
        return "d-none";
      }

      return this.hasServerError
        ? "text-danger text-left"
        : "text-success text-left";
    },
    validateForm() {
      let isValidFlag = true;
      this.errors = [];

      if (!this.reviewTitle) {
        this.errors.push("reviewTitle");
        isValidFlag = false;
      }

      if (!this.reviewDescription) {
        this.errors.push("reviewDescription");
        isValidFlag = false;
      }

      return isValidFlag;
    },
  },
  async created() {
    this.movie.id = this.$route.params.movieId;
    var response = await getMovie(this.movie.id);

    if (response.hasError) {
      console.log(response.errors);
      return;
    }

    console.log(response.data);

    this.movie.title = response.data.title;
    this.movie.language = response.data.language;
    this.movie.release = response.data.releaseDate;
    this.movie.plotSummary = response.data.plotSummary;
  },
};
</script>

<style scopped>
.form-group {
  align-items: left;
}
.form-group label {
  text-align: left;
  float: left;
}

.form-group small {
  text-align: center;
  float: none;
}
</style>
