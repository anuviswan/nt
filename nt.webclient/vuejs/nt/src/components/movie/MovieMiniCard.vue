<template>
  <div>
    <div class="card">
      <img
        class="card-img-top"
        src="https://cdn3.iconfinder.com/data/icons/avatars-flat/33/woman_9-512.png"
        alt="Movie Poster"
      />
      <div class="card-body">
        <h4 class="card-title">{{ this.title }}</h4>
        <p class="card-text">Language : {{ this.language }}</p>
        <p class="card-text">
          Released On : {{ getFormattedDate(new Date(this.releaseDate)) }}
        </p>
        <p class="card-text">Ratings : {{ this.ratings }}</p>
        <a href="#" class="btn btn-primary">See Reviews</a>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MovieMiniCard",
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
      ratings: 4.6,
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

<style scoped>
.card {
  width: 300px;
}
</style>
