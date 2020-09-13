<template>
  <nav class="navbar navbar-expand-lg navbar-light bg-light navbar-fixed-top py-0">
    <a class="navbar-brand" href="#">November Talkies</a>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarNav"
      aria-controls="navbarNav"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarNav">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
          <a class="nav-link" href="#">
            Home
            <span class="sr-only">(current)</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#">Users</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#">Movies</a>
        </li>
      </ul>
      <ul class="navbar-nav">
        <li class="nav-item active">
          <router-link v-bind:to="`user/${currentUser.userName}`">
            {{ currentUser.userName }}
            <span class="sr-only">(current)</span>
          </router-link>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "Navbar",
  computed: mapGetters(["currentUser"]),
};
</script>

<style scopped>
.navbar {
  padding-top: 0;
  padding-bottom: 0;
}
</style>
