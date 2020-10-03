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
            <form class="form needs-validation" @submit="onSubmit">
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
                <input
                  type="text"
                  v-model="displayName"
                  name="displayName"
                  v-bind:class="
                    hasError('displayName')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <label for="bio">Bio</label>
                <textarea
                  v-model="bio"
                  name="bio"
                  v-bind:class="
                    hasError('bio')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
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
      errors: [],
    };
  },
  methods: {
    onSubmit(e) {
      e.preventDefault();
      if (this.validateForm()) {
        console.log("no error");
      }
      console.log(this.errors);
    },
    hasError(key) {
      return this.errors.indexOf(key) != -1;
    },
    validateForm() {
      let isValidFlag = false;
      this.errors = [];

      if (!this.displayName) {
        this.errors.push("displayName");
        isValidFlag = false;
      }

      if (!this.bio) {
        this.errors.push("bio");
        isValidFlag = false;
      }

      return isValidFlag;
    },
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