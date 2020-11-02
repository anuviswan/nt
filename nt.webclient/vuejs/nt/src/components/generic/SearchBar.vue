<template>
  <form autocomplete="off" class="form-horizontal" v-on:submit="onSubmit">
    <div class="input-group">
      <input name="searchtext" value="" class="form-control" type="text" />
      <button class="btn btn-primary btn-rounded" type="submit">
        Search
      </button>
      <div>
        <label class="radio-inline"
          ><input type="radio" name="optradio" checked />User</label
        >
        <label class="radio-inline"
          ><input type="radio" name="optradio" />Movie</label
        >
      </div>
    </div>
  </form>
</template>

<script>
export default {
  name: "SearchBar",
  methods: {
    onSubmit(e) {
      e.preventDefault();
      this.$emit("searched", "KEY", "USER");
    },
  },
};
</script>

<style></style>
