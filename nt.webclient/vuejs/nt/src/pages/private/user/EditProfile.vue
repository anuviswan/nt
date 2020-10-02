<template>
  <div className="container-fluid ">
    <div className="row">
      <div className="col-sm-2 col-md-2 col-lg-2">
        <EditUserMenu />
      </div>
      <div className="col-sm-8 col-md-8 col-lg-10">
        The main contend would be displayed here
      </div>
    </div>
  </div>
</template>

<script>
import EditUserMenu from "../../../components/user/EditUserMenu";
export default {
  name: "EditProfile",
  components: {
    EditUserMenu,
  },
};
</script>

<style>
</style>