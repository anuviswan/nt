<template>
  <div>
    <span v-for="index in 5" :key="index">
      <i
        v-bind:class="index <= rating ? 'fa fa-star checked' : 'fa fa-star'"
      ></i>
    </span>
    <span class="small font-italic" v-if="isReadOnly"
      >({{ this.numberOfReviews }} reviews)</span
    >
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
    isReadOnly: {
      type: Boolean,
      default: true,
    },
    numberOfReviews: {
      type: Number,
      default: 23,
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
