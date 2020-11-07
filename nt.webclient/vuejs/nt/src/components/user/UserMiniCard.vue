<template>
  <div>
    <div class="card">
      <div class="row no-gutters">
        <div class="col-sm-5 bg-secondary">
          <div class="card">
            <div class="card-body">
              <div class="d-flex flex-column align-items-center text-center">
                <img
                  src="https://bootdey.com/img/Content/avatar/avatar7.png"
                  alt="Admin"
                  class="rounded-circle"
                  width="100"
                />
              </div>
            </div>
          </div>
        </div>
        <div class="col-sm-7">
          <div class="card-body">
            <div class="mt-3">
              <h4>{{ this.user.displayName }}</h4>
              <p class="text-secondary mb-1">6 Followers</p>
              <p class="text-muted font-size-sm">
                Rating: 5
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "UserMiniCard",
  props: {
    user: {
      required: true,
      type: Object,
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

  created() {
    this.userName = this.user.userName;
    this.displayName = this.user.displayName;
    this.bio = this.user.bio;
    this.rating = this.user.rating;
  },
};
</script>

<style scoped>
.card {
  max-width: 300px;
}
</style>
