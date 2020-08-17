<template>
  <div>
    <div class="row">
      <div class="col-lg-9">
        <h1>November Talkies</h1>
        <div class="text-danger">
          <i class="fas fa-5x fa-video" />
        </div>
      </div>
      <div class="col-lg-3">
        <Login />
      </div>
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

<style></style>
