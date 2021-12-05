<template>
  <span class="badge badge-secondary mx-1">{{ text }}</span>
</template>

<script>
export default {
  name: "Tags",
  props: {
    text: {
      required: true,
      type: String,
    },
  },
};
</script>

<style></style>
