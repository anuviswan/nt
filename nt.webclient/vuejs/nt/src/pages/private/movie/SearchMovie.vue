<template>
  <div>
    <div>
      <SearchBar v-on:searched="onSearch" promptString="Search Movies..." />
    </div>
    <div v-if="movieList.length">
      <UserList v-bind:userList="movieList" />
    </div>
  </div>
</template>

<script>
import SearchBar from "../../../components/generic/SearchBar";
import { searchMovieByTitle } from "../../../api/movies";
export default {
  name: "SearchMovie",
  components: { SearchBar },
  data() {
    return {
      movieList: [],
    };
  },
  methods: {
    async onSearch(key) {
      console.log(key);
      var response = await searchMovieByTitle(key, -1);
      console.log(response);
    },
  },
};
</script>

<style></style>
