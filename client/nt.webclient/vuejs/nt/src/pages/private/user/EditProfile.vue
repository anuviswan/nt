<template>
  <div class="container-fluid">
    <div class="row align-items-center justify-content-center">
      <div class="col-sm-12 col-md-10 col-lg-8">
        <div class="card">
          <div class="card-header font-weight-bold text-uppercase">
            Edit User Profile
          </div>
          <div class="card-body">
            <form
              class="form-horizontal needs-validation"
              v-on:submit="onSubmit"
            >
              <div class="form-group">
                <label for="userName">User Name</label>
                <input
                  type="text"
                  v-bind:placeholder="userName"
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
                <input type="submit" value="Save Changes" class="btn-primary" />
              </div>
              <div v-bind:class="showServerMessage()">
                <ul>
                  <li v-for="(error, index) in serverMessage" :key="index">
                    <small>{{ error }}</small>
                  </li>
                </ul>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapActions } from "vuex";
import { updateUserProfile } from "../../../api/user";
export default {
  name: "EditProfile",
  data() {
    return {
      authToken: "",
      userName: "sampleUserName",
      displayName: "sample Display Name",
      bio: "sample Bio",
      errors: [],
      serverMessage: [],
      hasServerError: false,
    };
  },
  created: function() {
    this.userName = this.$route.params.userid;
    this.authToken = this.currentUser.token;
    this.displayName = this.currentUser.displayName;
    this.bio = this.currentUser.bio;
  },
  computed: mapGetters(["currentUser"]),
  methods: {
    ...mapActions(["updateCurrentUser"]),
    async onSubmit(e) {
      e.preventDefault();
      if (this.validateForm()) {
        console.log("User Profile Update:Submiting data....");

        const userRecordToUpdate = {
          displayName: this.displayName,
          bio: this.bio,
        };

        var response = await updateUserProfile(userRecordToUpdate);

        if (response.hasError) {
          this.hasServerError = true;
          this.serverMessage = response.error;
          return;
        }
        this.updateCurrentUser({
          userName: this.userName,
          displayName: this.displayName,
          bio: this.bio,
          token: this.authToken,
        });

        this.serverMessage.push("User Profile updated successfully");
      }
    },
    hasError(key) {
      return this.errors.indexOf(key) != -1;
    },
    validateForm() {
      let isValidFlag = true;
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
    showServerMessage() {
      if (!this.serverMessage.length) {
        return "d-none";
      }

      return this.hasServerError
        ? "text-danger text-left"
        : "text-success text-left";
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

.form-group small {
  text-align: center;
  float: none;
}
</style>
