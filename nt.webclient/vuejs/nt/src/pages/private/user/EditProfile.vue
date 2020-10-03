<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-2 col-md-2 col-lg-2">
        <EditUserMenu />
      </div>
      <div class="col-sm-8 col-md-8 col-lg-10">
        <div class="card">
          <div class="card-header font-weight-bold text-uppercase">
            Edit User Profile
          </div>
          <div class="card-body">
            <form class="form needs-validation">
              <div class="form-group">
                <label for="userName">User Name</label>
                <input
                  type="text"
                  v-model="userName"
                  name="userName"
                  readOnly
                />
              </div>
              <div class="form-group">
                <label for="displayName">Display Name</label>
                <input type="text" v-model="displayName" name="displayName" />
              </div>
              <div class="form-group">
                <label for="bio">Bio</label>
                <textarea v-model="bio" name="bio" />
              </div>
              <div class="form-group">
                <input type="submit" value="Update" />
              </div>
            </form>
          </div>
        </div>
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
  data() {
    return {
      userName: "sampleUserName",
      displayName: "sample Display Name",
      bio: "sample Bio",
    };
  },
};
</script>

<style scoped>
input[type="text"],
input[type="email"],
input[type="password"],
input[type="date"],
select,
textarea {
  display: block;
  width: 100%;
  padding: 0.4rem;
  font-size: 1.2rem;
  border: 1px solid #ccc;
}

.form-group {
  align-items: left;
}
.form-group label {
  text-align: left;
  float: left;
}
</style>