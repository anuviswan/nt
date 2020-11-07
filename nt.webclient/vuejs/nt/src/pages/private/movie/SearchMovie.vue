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
export default {
  name: "SearchMovie",
  components: { SearchBar },
  data() {
    return {
      movieList: [],
    };
  },
  methods: {
    onSearch(key) {
      console.log(key);
    },
  },
};
</script>

<style></style>
