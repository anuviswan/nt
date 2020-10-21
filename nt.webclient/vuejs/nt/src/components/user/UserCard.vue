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
      {{ this.selectedUser }}
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
import { getUser } from '../../api/user'

export default {
  name: "UserCard",
  props: {
    userName: {
      required: true,
    },
  },
  data() {
    return {
      selectedUser: "",
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
  async created() {
    this.selectedUser = this.userName;

    var response = await getUser(this.selectedUser);
    if(response.hasError){
    console.log(response.errors);
    return;
    }
    this.displayName = response.data.displayName;
      this.bio = response.data.bio;
      this.rating = response.data.rating;

  },
};
</script>

<style>
</style>