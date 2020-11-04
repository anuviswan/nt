<template>
  <div class="card border-primary mb-3">
    <router-link
      exact
      v-bind:to="`/p/user/${this.userName}/edit`"
      v-if="isEditable()"
      >Edit User</router-link
    >
    <div class="card-avataar">
      <img
        class="card-img-top rounded-circle img-thumbnail"
        src="https://cdn3.iconfinder.com/data/icons/avatars-flat/33/woman_9-512.png"
        alt="Avataar Pic"
      />
    </div>

    <div class="card-body mx-auto">
      <h4 class="card-title block text-uppercase text-center">
        {{ this.displayName }}
      </h4>
      {{ this.userName }}
      <!-- <Rating value="{5}" totalStars="{5}" /> -->
    </div>

    <div class="card-header">
      <div class="row">
        <div class="col-lg-4 card-metadata text-center">45</div>
        <div class="col-lg-4 card-metadata text-center">345</div>
        <div class="col-lg-4 card-metadata text-center">1.1K</div>
      </div>
      <div class="row">
        <div class="col-lg-4 card-metadata-footer text-center">Reviews</div>
        <div class="col-lg-4 card-metadata-footer text-center">Followed</div>
        <div class="col-lg-4 card-metadata-footer text-center">Followers</div>
      </div>
    </div>
    <div class="card-body mx-auto">{{ this.bio }}</div>
  </div>
</template>

<script>
import { mapGetters } from "vuex";

export default {
  name: "UserCard",
  props: {
    user: {
      required: true,
    },
  },
  data() {
    return {
      userName: "",
      displayName: "",
      bio: "",
      rating: 0,
    };
  },
  methods: {
    isEditable() {
      return this.userName == this.currentUser.userName;
    },
  },
  computed: mapGetters(["currentUser"]),
  created() {
    console.log("In usercard" + this.user);
    this.userName = this.user.userName;
    this.displayName = this.user.displayName;
    this.bio = this.user.bio;
    this.rating = this.user.rating;
  },
};
</script>

<style></style>
