<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-2 col-md-2 col-lg-2">
        <EditUserMenu />
      </div>
      <div class="col-sm-8 col-md-8 col-lg-10">
        <div>Change Password here</div>
      </div>
    </div>
  </div>
</template>

<script>
import EditUserMenu from "../../../components/user/EditUserMenu.vue";
export default {
  name: "ChangePassword",
  components: {
    EditUserMenu,
  },
};
</script>

<style>
</style>