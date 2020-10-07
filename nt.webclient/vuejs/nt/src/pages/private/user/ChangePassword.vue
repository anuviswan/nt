<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-2 col-md-2 col-lg-2">
        <EditUserMenu />
      </div>
      <div class="col-sm-8 col-md-8 col-lg-10">
        <div class="card">
          <div class="card-header font-weight-bold text-uppercase">
            Change Password
          </div>
          <div class="card-body">
            <form class="form needs-validation" v-on:submit="onSubmit">
              <div class="form-group">
                <label for="oldPassword">Old Password</label>
                <input
                  type="password"
                  v-model="oldPassword"
                  name="oldPassword"
                  v-bind:class="
                    hasError('oldPassword')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>

              <div class="form-group">
                <label for="newPassword">New Password</label>
                <input
                  type="text"
                  name="newPassword"
                  v-model="newPassword"
                  v-bind:class="
                    hasError('newPassword')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>

              <div class="form-group">
                <label for="confirmPassword">Confirm Password</label>
                <input
                  type="text"
                  name="confirmPassword"
                  v-model="confirmPassword"
                  v-bind:class="
                    hasError('confirmPassword')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>

              <div class="form-group">
                <input type="submit" value="Update Password" />
              </div>
              <div class="form-group">
                <div v-bind:class="showServerMessage()">
                  <small>{{ this.serverMessage }}</small>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EditUserMenu from "../../../components/user/EditUserMenu.vue";
import axios from "axios";
import { mapGetters } from "vuex";
export default {
  name: "ChangePassword",
  data() {
    return {
      authToken: "",
      oldPassword: "",
      newPassword: "",
      confirmPassword: "",
      errors: [],
      serverMessage: "",
      hasServerError: false,
    };
  },
  components: {
    EditUserMenu,
  },
  computed: mapGetters(["currentUser"]),
  created: function () {
    this.authToken = this.currentUser.token;
  },
  methods: {
    async onSubmit(e) {
      e.preventDefault();
      if (this.validateForm()) {
        console.log("Changing Password....");

        const headers = {
          "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
          "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
          "Content-Type": "application/json", // this shows the expected content type
          Authorization: `Bearer ${this.authToken}`,
        };
        var updatedRecord = {
          oldPassword: this.oldPassword,
          newPassword: this.newPassword,
        };
        try {
          var response = await axios.post(
            "https://localhost:44353/api/User/ChangePassword",
            updatedRecord,
            { headers: headers }
          );

          console.log(response);
          if (response.data.hasError) {
            this.hasServerError = response.data.hasError;
            this.serverMessage = response.data.errorMessage;
            return;
          }

          this.hasServerError = false;
          this.serverMessage = "Password has been changed successfully.";
        } catch (error) {
          this.serverMessage = error.response.data.errors.NewPassword.shift();
          this.hasServerError = true;
          return;
        }
      }
    },
    hasError(key) {
      console.log(key + (this.errors.indexOf(key) != -1));
      return this.errors.indexOf(key) != -1;
    },
    validateForm() {
      let isValidFlag = true;
      this.errors = [];

      if (!this.newPassword) {
        this.errors.push("newPassword");
        isValidFlag = false;
      }

      if (!this.confirmPassword) {
        this.errors.push("confirmPassword");
        isValidFlag = false;
      }

      if (this.newPassword != this.confirmPassword) {
        this.errors.push("newPassword");
        this.errors.push("confirmPassword");
        isValidFlag = false;
      }

      return isValidFlag;
    },
    showServerMessage() {
      console.log(this.serverMessage);
      if (!this.serverMessage) {
        return "d-none justify-content-center";
      }

      return this.hasServerError
        ? "text-danger justify-content-center"
        : "text-success justify-content-center";
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