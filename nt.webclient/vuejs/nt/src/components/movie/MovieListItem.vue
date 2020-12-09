<template>
  <div>
    <div class="card">
      <div class="card-body">
        <div class="row">
          <!-- First Column -->
          <div class="col-5">
            <div class="card-text text-left">
              <h5>
                <strong>{{ this.title }}</strong>
              </h5>
            </div>
            <div class="row">
              <div class="col-3 card-text text-left small text-capitalize">
                {{ this.language }}
              </div>
              <div class="col-3 card-text text-left small text-capitalize">
                {{ this.releaseDate.getFullYear() }}
              </div>
              <div class="col-3 card-text text-left small text-capitalize">
                {{ this.genre }}
              </div>
            </div>
            <div class="card-text text-left">
              <br />
              {{ this.plotSummary }}
            </div>
          </div>
          <!-- Second Column -->
          <div class="col-3">
            <div class="card-text text-left">
              Released On : {{ getFormattedDate(new Date(this.releaseDate)) }}
            </div>
            <div class="card-text text-left">
              Director : {{ this.director }}
            </div>
            <div class="card-text text-left">
              <Star-rating
                v-bind:inline="true"
                v-bind:star-size="15"
                v-bind:read-only="true"
                v-bind:fixed-points="1"
                v-bind:increment="0.5"
                v-model="rating"
                v-bind:animate="true"
                v-bind:show-rating="false"
                text-class="invisible"
              /><span class="small font-italic"
                >({{ this.totalReviews }} reviews)</span
              >
            </div>
          </div>
          <!-- Third Column -->
          <div class="col-3">
            <p><a href="#" class="btn btn-primary">All Reviews</a></p>
            <p>
              <router-link
                v-bind:to="`/p/movie/addreview/${this.Id}`"
                class="btn btn-primary"
                >Add Review</router-link
              >
            </p>
          </div>
        </div>
      </div>
      <div class="card-footer text-left">
        <span
          class="card-text text-left"
          v-for="(tag, index) in tags"
          :key="index"
        >
          <tags v-bind:text="tag" />
        </span>
      </div>
    </div>
  </div>
</template>

<script>
import StarRating from "vue-star-rating";
import Tags from "../generic/Tags";
export default {
  name: "MovieListItem",
  components: { StarRating, Tags },
  props: {
    movie: {
      required: true,
      type: Object,
    },
  },
  data() {
    return {
      Id: "",
      title: "Default Title",
      language: "Default Language",
      releaseDate: new Date(),
      rating: 4,
      genre: "Thriller",
      tags: ["John Doe", "Jia Anu", "Naina Anu"],
      movieMeta: { m: "s" },
      totalReviews: 43,
      plotSummary: "",
      director: "",
    };
  },
  created() {
    this.Id = this.movie.id;
    this.title = this.movie.title;
    this.language = this.movie.language;
    this.releaseDate = new Date(this.movie.releaseDate);
    this.plotSummary = this.movie.plotSummary;
    this.totalReviews = this.movie.totalReviews;
    this.rating = this.movie.rating;
    this.genre = this.movie.genre;
    this.tags = this.movie.castAndCrew;
    this.director = this.movie.director;
    console.log(this.movie);
  },
  methods: {
    getFormattedDate(date) {
      let year = date.getFullYear();
      let month = (1 + date.getMonth()).toString().padStart(2, "0");
      let day = date
        .getDate()
        .toString()
        .padStart(2, "0");

      return day + "/" + month + "/" + year;
    },
  },
};
</script>

<style scoped></style>
