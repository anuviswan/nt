<template>
  <div>
    <div>
      <SearchBar v-on:searched="onSearch" promptString="Search Movies..." />
    </div>
    <div class="row">
      <div
        v-for="(movie, index) in movieList"
        :key="index"
        class="col-md-3 col-6 pt-4"
      >
        <MovieMiniCard v-bind:movie="movie" />
      </div>
    </div>
  </div>
</template>

<script>
import SearchBar from "../../../components/generic/SearchBar";
import MovieMiniCard from "../../../components/movie/MovieMiniCard";
import { searchMovieByTitle } from "../../../api/movies";

export default {
  name: "SearchMovie",
  components: { SearchBar, MovieMiniCard },
  data() {
    return {
      movieList: [],
    };
  },
  methods: {
    async onSearch(key) {
      console.log(key);
      var response = await searchMovieByTitle(key, -1);
      if (response.hasError) {
        this.hasServerError = true;
        this.serverMessage = response.error;
        return;
      }

      this.movieList = response.data;
    },
  },
};
</script>

<style></style>
