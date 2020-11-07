<template>
  <form autocomplete="off" class="form-horizontal" v-on:submit="onSubmit">
    <div class="input-group">
      <input
        type="text"
        class="form-control"
        v-bind:placeholder="promptString"
        v-model="searchKey"
        name="searchtext"
      />
      <div class="input-group-append">
        <button class="btn btn-primary" type="submit">
          <i class="fa fa-search"></i>
        </button>
      </div>
    </div>

    <!-- <div class="input-group">
      <input
        name="searchtext"
        class="form-control"
        v-bind:placeholder="promptString"
        type="text"
        v-model="searchKey"
      />
      <button class="btn btn-primary btn-rounded" type="submit">
        Search
      </button>
    </div> -->
  </form>
</template>

<script>
export default {
  name: "SearchBar",
  props: {
    promptString: {
      required: true,
      type: String,
    },
  },
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
