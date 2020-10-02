<template>
  <div class="card border-primary mb-3">
    Edit User
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
import Axios from "axios";
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
  async created() {
    this.selectedUser = this.userName;
    console.log(this.selectedUser);

    var response = await Axios.get("https://localhost:44353/api/User/GetUser", {
      params: { userName: this.selectedUser },
    });
    console.log(response);
    if (!response.data.hasError) {
      this.displayName = response.data.displayName;
      this.bio = response.data.bio;
      this.rating = response.data.rating;
    }
  },
};
</script>

<style>
</style>