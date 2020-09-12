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
      <h4 class="card-title block text-uppercase text-center">Display Name</h4>User Name
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
    <div class="card-body mx-auto"></div>
  </div>
</template>

<script>
export default {
  name: "UserCard",
};
</script>

<style>
</style>