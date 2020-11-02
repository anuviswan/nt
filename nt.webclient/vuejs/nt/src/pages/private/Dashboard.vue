<template>
  <div>
    <SearchBar v-on:searched="onSearch" />
  </div>
</template>

<script>
import SearchBar from "../../components/generic/SearchBar";
export default {
  name: "Dashboard",
  components: { SearchBar },
  methods: {
    onSearch(key, searchType) {
      console.log("search initiated");
      console.log(key);
      console.log(searchType);
    },
  },
};
</script>

<style></style>
