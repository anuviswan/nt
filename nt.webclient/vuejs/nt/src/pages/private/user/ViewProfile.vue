<template>
  <div class="row">
    <div class="col-md-4 col-lg-2 col-lx-2">
      <UserCard :user="this.userName" />
    </div>
    <div class="col-md-8 col-lg-10 col-lx-10">
      <div class="card">
        <div class="card-header">Movie Reviews</div>
        <div class="card-body">
          <h5 class="card-title">Dil Bechara</h5>
          <p class="card-text text-wrap">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean
            nec placerat neque. Praesent ipsum sem, posuere at tortor sed,
            pharetra vestibulum metus. Suspendisse id porttitor tortor.
            Praesent efficitur ante lectus, a suscipit mi mollis eget. Sed eu
            purus vel mauris laoreet tempus eu non turpis. Nam in porttitor
            erat. Mauris non lorem mi. Praesent ac hendrerit quam.
          </p>
          <a href="/more" class="btn btn-info">more</a>
        </div>
        <div class="card-body">
          <h5 class="card-title">Life of Pie</h5>
          <p class="card-text">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean
            nec placerat neque. Praesent ipsum sem, posuere at tortor sed,
            pharetra vestibulum metus. Suspendisse id porttitor tortor.
            Praesent efficitur ante lectus, a suscipit mi mollis eget. Sed eu
            purus vel mauris laoreet tempus eu non turpis. Nam in porttitor
            erat. Mauris non lorem mi. Praesent ac hendrerit quam.
          </p>
          <a href="/more" class="btn btn-info">more</a>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import UserCard from "../../../components/user/UserCard";
export default {
  name: "ViewProfile",
  components: { UserCard },
  data() {
    return {
      userName: "",
    };
  },
  created: function () {
    // console.log(this.$route.params.userid);
    // this.userName = this.$route.params.userid;
  },
};
</script>

<style>
</style>