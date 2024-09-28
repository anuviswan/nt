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
              v-bind:to="`/p/movie/create`"
            >
              Create
            </router-link>
          </div>
        </li>
      </ul>

      <div class="d-flex">
        <div class="input-group">
          <input
            class="form-control me-2"
            type="search"
            v-model="searchTerm"
            placeholder="Search"
            aria-label="Search"
          />
          <div class="input-group-append">
            <button
              class="btn btn-outline-light"
              type="button"
              @click="onSearch"
            >
              Search
            </button>
          </div>
        </div>
      </div>

      <ul class="navbar-nav">
        <li class="nav-item dropdown active">
          <a
            class="nav-link dropdown-toggle"
            href="#"
            id="navbardrop"
            data-toggle="dropdown"
          >
            {{ currentUser.displayName }}
          </a>
          <div class="dropdown-menu dropdown-menu-right">
            <router-link
              class="dropdown-item"
              exact
              v-bind:to="`/p/user/${currentUser.userName}/view`"
            >
              View Profile
            </router-link>
            <router-link exact v-bind:to="`/p/user/edit`" class="dropdown-item">
              Edit User
            </router-link>
            <router-link
              class="dropdown-item"
              exact
              v-bind:to="`/p/user/${currentUser.userName}/chpwd`"
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

<script setup lang="ts">
import { ref } from "vue";
import { useUserStore } from "@/stores/userStore";
import router from "@/router";
import routesNames from "@/router/routeNames";

const store = useUserStore();
const currentUser = ref({
  userName: store.UserName,
  displayName: store.DisplayName,
  bio: store.Bio,
});

const searchTerm = ref<string>("");

const logout = (): void => {
  console.log("logging out");
  store.Reset();
  router.push("/");
};

const onSearch = (): void => {
  router.push({
    name: routesNames.searchPage.name,
    params: { searchTerm: searchTerm.value },
  });
  searchTerm.value = "";
};
</script>

<style scoped></style>
