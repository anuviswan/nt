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
              This is a mock plot of the movie, which would be displayed in the
              movie card
            </div>
          </div>
          <!-- Second Column -->
          <div class="col-3">
            <div class="card-text text-left">
              Genre : Sci-fi
            </div>
            <div class="card-text text-left">
              Released On : {{ getFormattedDate(new Date(this.releaseDate)) }}
            </div>
            <div class="card-text text-left"><Ratings rating="4" /></div>
          </div>
          <!-- Third Column -->
          <div class="col-3">
            <p><a href="#" class="btn btn-primary">See Reviews</a></p>
            <p>
              <router-link
                v-bind:to="`/p/movie/addreview/${this.userName}`"
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
import Ratings from "../generic/Ratings";
import Tags from "../generic/Tags";
export default {
  name: "MovieListItem",
  components: { Ratings, Tags },
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
      ratings: 4,
      genre: "Thriller",
      tags: ["John Doe", "Jia Anu", "Naina Anu"],
      movieMeta: { m: "s" },
    };
  },
  created() {
    this.Id = this.movie.Id;
    this.title = this.movie.title;
    this.language = this.movie.language;
    this.releaseDate = new Date(this.movie.releaseDate);

    console.log(this.title);
    console.log(this.releaseDate);
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
