<template>
  <div>
    <Navbar />
    <div class="pt-5">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import Navbar from "../../components/Navbar";
export default {
  name: "PrivateContainer",
  components: {
    Navbar,
  },
};
</script>

<style>
</style>