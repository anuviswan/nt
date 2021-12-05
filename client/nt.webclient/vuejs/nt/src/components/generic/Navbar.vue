<template>
  <nav
    class="navbar navbar-expand-lg navbar-dark bg-primary navbar-fixed-top py-0"
  >
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
          <router-link class="nav-link" exact to="/p/dashboard"
            >Home</router-link
          >
        </li>
        <li class="nav-item">
          <router-link class="nav-link" exact to="/p/UserSearch"
            >Users</router-link
          >
        </li>
        <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle"
            href="#"
            id="navbardrop"
            data-toggle="dropdown"
          >
            Movies
          </a>
          <div class="dropdown-menu">
            <router-link
              class="dropdown-item"
              exact
              v-bind:to="`/p/movie/search`"
            >
              Search
            </router-link>
            <router-link
              class="dropdown-item"
              exact
              v-bind:to="`/p/movie/create`"
            >
              Create
            </router-link>
          </div>
        </li>
      </ul>

      <ul class="navbar-nav">
        <li class="nav-item dropdown active">
          <a
            class="nav-link dropdown-toggle"
            href="#"
            id="navbardrop"
            data-toggle="dropdown"
          >
            {{ currentUser.userName }}
          </a>
          <div class="dropdown-menu dropdown-menu-right">
            <router-link
              class="dropdown-item"
              exact
              v-bind:to="`/p/user/${currentUser.userName}`"
            >
              View Profile
            </router-link>
            <router-link
              exact
              v-bind:to="`/p/user/${currentUser.userName}/edit`"
              class="dropdown-item"
            >
              Edit User
            </router-link>
            <router-link class="dropdown-item" to="./chpwd"
              >Change Password</router-link
            >
            <div class="dropdown-divider"></div>
            <label class="dropdown-item" href="#" v-on:click="logout"
              >Log out</label
            >
          </div>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
import { mapGetters, mapActions } from "vuex";

export default {
  name: "Navbar",
  computed: mapGetters(["currentUser"]),
  methods: {
    ...mapActions(["clearUser"]),
    logout() {
      this.clearUser();
      this.$router.push("/");
    },
  },
};
</script>

<style scoped>
.navbar {
  padding-top: 0;
  padding-bottom: 0;
}
.navbar-brand {
  font-family: "Pacifico";
  /* color: grey; */
  margin: 0px;
}
</style>
