<template>
  <div class="row">
    <div v-for="index in 5" :key="index">
      <i
        v-bind:class="index <= rating ? 'fa fa-star checked' : 'fa fa-star'"
      ></i>
    </div>
  </div>
</template>

<script>
export default {
  name: "Ratings",
  props: {
    rating: {
      required: true,
      type: Number,
    },
  },
  created() {
    console.log(this.rating);
  },
};
</script>

<style>
.checked {
  color: orange;
}
</style>
