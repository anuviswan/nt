<template>
  <div>
    <div class="card">
      <div class="row">
        <div class="col-10 card-header">
          <h4>{{ this.title }}</h4>
        </div>
        <div class="col-2 card-header">
          <Ratings v-bind:rating="this.ratings" />
        </div>
      </div>

      <div class="card-body">
        <div class="row">
          <!-- First Column -->
          <div class="col-3">
            <div class="card-text text-left">
              This is a mock plot of the movie, which would be displayed in the
              movie card
            </div>
          </div>
          <!-- Second Column -->
          <div class="col-3">
            <div class="card-text text-left">
              Language : {{ this.language }}
            </div>
            <div class="card-text text-left">
              Genre : Sci-fi
            </div>
            <div class="card-text text-left">
              Released On : {{ getFormattedDate(new Date(this.releaseDate)) }}
            </div>
          </div>
          <!-- Third Column -->
          <div class="col-3">
            <div class="card-text text-left">
              Genre : Sci-fi
            </div>
            <div class="card-text text-left">
              Released On : {{ getFormattedDate(new Date(this.releaseDate)) }}
            </div>
          </div>
        </div>

        <p><a href="#" class="btn btn-primary">See Reviews</a></p>
        <p>
          <router-link to="/p/movie/addreview" class="btn btn-primary"
            >Add Review</router-link
          >
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import Ratings from "../../components/generic/Ratings";
export default {
  name: "MovieMiniCard",
  components: { Ratings },
  props: {
    movie: {
      required: true,
      type: Object,
    },
  },
  data() {
    return {
      title: "Default Title",
      language: "Default Language",
      releaseDate: "20/08/2020",
      ratings: 4,
    };
  },
  created() {
    this.title = this.movie.title;
    this.language = this.movie.language;
    this.releaseDate = this.movie.releaseDate;

    console.log(this.title);
    console.log(this.language);
  },
  methods: {
    getFormattedDate(date) {
      let year = date.getFullYear();
      let month = (1 + date.getMonth()).toString().padStart(2, "0");
      let day = date
        .getDate()
        .toString()
        .padStart(2, "0");

      return month + "/" + day + "/" + year;
    },
  },
};
</script>

<style scoped></style>
