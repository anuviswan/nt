<template>
  <div class="container-fluid">
    <slot></slot>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component
export default class DefaultTemplate extends Vue {}
</script>

<style scoped>
.login,
.image {
  min-height: 100vh;
}

.bg-image {
  background-image: url("https://res.cloudinary.com/mhmd/image/upload/v1555917661/art-colorful-contemporary-2047905_dxtao7.jpg");
  background-size: cover;
  background-position: center center;
}

.bg-indigo {
  background: #4b1076;
}

.navbar-brand {
  font-family: "Pacifico";
  font-size: xx-large;
  margin: 0px;
}
</style>
