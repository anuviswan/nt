<template>
  <div class="row">
    <div class="col-3">
      Here Movie
    </div>
    <div class="col-9">
      Here Review
    </div>
  </div>
</template>

<script>
export default {
  name: "CreateReview",
  props: {
    movie: {
      type: String,
      required: true,
    },
  },
};
</script>

<style></style>
