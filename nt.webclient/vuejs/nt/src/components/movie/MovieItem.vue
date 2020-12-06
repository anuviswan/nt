<template>
  <div>
    <div class="card">
      <div class="card-body">
        <div class="row">
          <!-- First Column -->
          <div class="col-12">
            <div class="card-text text-left">
              <h5>
                <strong>{{ this.title }}</strong>
              </h5>
              <div class="card-text text-left">
                <Star-rating
                  inline="true"
                  star-size="15"
                  read-only="true"
                  :rating="ratings"
                  animate="true"
                  show-rating="false"
                  text-class="invisible"
                />
                <span class="small font-italic"
                  >({{ this.numberOfReviews }} reviews)</span
                >
              </div>
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
              <div>{{ this.plotSummary }}</div>
            </div>
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
import Tags from "../generic/Tags";
import StarRating from "vue-star-rating";

export default {
  name: "MovieItem",
  components: { Tags, StarRating },
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
      numberOfReviews: 4,
      plotSummary: "",
    };
  },
  created() {
    this.Id = this.movie.Id;
    this.title = this.movie.title;
    this.language = this.movie.language;
    this.releaseDate = new Date(this.movie.releaseDate);
    this.plotSummary = this.movie.plotSummary;

    console.log("reloaded");
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

      return month + "/" + day + "/" + year;
    },
  },
};
</script>

<style scoped></style>
