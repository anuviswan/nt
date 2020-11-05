<template>
  <!-- <nav class="mb-1 navbar navbar-expand-lg navbar-dark default-color">
    <a class="navbar-brand" href="#">Navbar</a>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarSupportedContent-3"
      aria-controls="navbarSupportedContent-3"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent-3">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item active">
          <a class="nav-link waves-effect waves-light" href="#"
            >Home
            <span class="sr-only">(current)</span>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link waves-effect waves-light" href="#">Features</a>
        </li>
        <li class="nav-item">
          <a class="nav-link waves-effect waves-light" href="#">Pricing</a>
        </li>
        <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle waves-effect waves-light"
            id="navbarDropdownMenuLink-3"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
            >Dropdown
          </a>
          <div
            class="dropdown-menu dropdown-default"
            aria-labelledby="navbarDropdownMenuLink-3"
          >
            <a class="dropdown-item waves-effect waves-light" href="#"
              >Action</a
            >
            <a class="dropdown-item waves-effect waves-light" href="#"
              >Another action</a
            >
            <a class="dropdown-item waves-effect waves-light" href="#"
              >Something else here</a
            >
          </div>
        </li>
      </ul>
      <ul class="navbar-nav ml-auto nav-flex-icons">
        <li class="nav-item">
          <a class="nav-link waves-effect waves-light">
            <i class="fab fa-twitter"></i>
          </a>
        </li>
        <li class="nav-item">
          <a class="nav-link waves-effect waves-light">
            <i class="fab fa-google-plus-g">User</i>
          </a>
        </li>
        <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle waves-effect waves-light"
            id="navbarDropdownMenuLink"
            data-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            <i class="fas fa-user"></i>
          </a>
          <div
            class="dropdown-menu dropdown-menu-right dropdown-default"
            aria-labelledby="navbarDropdownMenuLink"
          >
            <a class="dropdown-item waves-effect waves-light" href="#"
              >Action</a
            >
            <a class="dropdown-item waves-effect waves-light" href="#"
              >Another action</a
            >
            <a class="dropdown-item waves-effect waves-light" href="#"
              >Something else here</a
            >
          </div>
        </li>
      </ul>
    </div>
  </nav> -->

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
        <li class="nav-item dropdown">
          <a
            class="nav-link dropdown-toggle"
            href="#"
            id="navbardrop"
            data-toggle="dropdown"
          >
            Dropdown link
          </a>
          <div class="dropdown-menu">
            <a class="dropdown-item" href="#">Link 1</a>
            <a class="dropdown-item" href="#">Link 2</a>
            <a class="dropdown-item" href="#">Link 3</a>
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
            <a class="dropdown-item" href="#">Link 2</a>
            <a class="dropdown-item" href="#">Link 3</a>
          </div>
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
