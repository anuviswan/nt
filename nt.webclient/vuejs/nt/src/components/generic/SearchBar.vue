<template>
  <form autocomplete="off" class="form-horizontal" v-on:submit="onSubmit">
    <div class="input-group">
      <input
        name="searchtext"
        class="form-control"
        type="text"
        v-model="searchKey"
      />
      <button class="btn btn-primary btn-rounded" type="submit">
        Search
      </button>
    </div>
  </form>
</template>

<script>
export default {
  name: "SearchBar",
  data() {
    return {
      searchKey: "",
    };
  },
  methods: {
    onSubmit(e) {
      e.preventDefault();
      this.$emit("searched", this.searchKey);
    },
  },
};
</script>

<style></style>
