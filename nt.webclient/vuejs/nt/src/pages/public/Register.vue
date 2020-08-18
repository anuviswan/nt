<template>
  <div class="row">
    <div class="col-lg-9">More details will come here - UNDER CONSTRUCTION</div>
    <div class="col-lg-3">
      <div class="card card-block rounded shadow shadow-sm">
        <div class="card-header bg-primary text-light text-uppercase">
          <div class="card-title align-middle">
            <h5 class="mb-0">Sign Up</h5>
          </div>
        </div>
        <div class="card-body">
          <form class="form needs-validation" @submit="onSubmit">
            <div class="form-group">
              <input
                type="text"
                v-model="userName"
                v-bind:class="
                  hasError('userName')
                    ? 'form-control block is-invalid'
                    : 'form-control block'
                "
                placeholder="Username"
              />
            </div>
            <div
              v-bind:class="
                hasError('userName') ? 'text-danger text-left' : 'd-none'
              "
            >
              <small>Username cannot be empty</small>
            </div>
            <div class="form-group">
              <input
                type="password"
                v-model="password"
                v-bind:class="
                  hasError('password')
                    ? 'form-control block is-invalid'
                    : 'form-control block'
                "
                placeholder="Password"
              />
            </div>
            <div
              v-bind:class="
                hasError('password') ? 'text-danger text-left' : 'd-none'
              "
            >
              <small>Password cannot be empty</small>
            </div>
            <div class="form-group">
              <input
                type="password"
                v-model="confirmPassword"
                v-bind:class="
                  hasError('confirmPassword')
                    ? 'form-control block is-invalid'
                    : 'form-control block'
                "
                placeholder="Confirm Password"
              />
            </div>
            <div
              v-bind:class="
                hasError('confirmPassword') ? 'text-danger text-left' : 'd-none'
              "
            >
              <small>Password does not match</small>
            </div>
            <div class="form-group">
              <input type="submit" class="btn btn-block btn-primary" value="Submit" />
            </div>
          </form>
          <div>
            Not a member ?
            <router-link to="/register">Sign up here</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Register",
  data() {
    return {
      errors: [],
      userName: "",
      password: "",
      confirmPassword: "",
    };
  },
  methods: {
    hasError(key) {
      return this.errors.indexOf(key) != -1;
    },
    async onSubmit(e) {
      e.preventDefault();

      if (!this.validateForm()) {
        // has Error
        console.log(this.userName);
        return;
      }

      const userDetails = {
        userName: this.userName,
        passKey: this.password,
        displayName: this.userName,
      };
      const response = await axios.post(
        "https://localhost:44353/api/User/CreateUser",
        userDetails
      );

      console.log(response.data);
    },
    validateForm() {
      let isValidFlag = true;
      this.errors = [];
      if (!this.userName) {
        this.errors.push("userName");
        isValidFlag = false;
      }

      if (!this.password) {
        this.errors.push("password");
        isValidFlag = false;
      }

      if (this.password != this.confirmPassword) {
        console.log(this.password);
        console.log(this.confirmPassword);
        this.errors.push("confirmPassword");
        isValidFlag = false;
      }
      return isValidFlag;
    },
  },
};
</script>

<style></style>
