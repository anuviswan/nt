<template>
  <div class="row">
    <div class="col-lg-9">
      <h1>November Talkies</h1>
    </div>
    <div class="col-lg-3">
      <Login />
    </div>
  </div>
</template>

<script>
import Login from "../components/Login";
export default {
  name: "Home",
  components: {
    Login,
  },
};
</script>

<style>
</style>