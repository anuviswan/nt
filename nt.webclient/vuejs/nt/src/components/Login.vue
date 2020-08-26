<template>
  <div class="card card-block rounded shadow shadow-sm">
    <div class="card-header bg-primary text-light text-uppercase">
      <div class="card-title align-middle">
        <h5 class="mb-0">Sign In</h5>
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
          <input type="submit" class="btn btn-block btn-primary" value="Submit" />
        </div>
        <div v-bind:class="showServerMessage()">
          <small>{{serverMessage}}</small>
        </div>
      </form>
      <div>
        Not a member ?
        <router-link to="/register">Sign up here</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Login",
  data() {
    return {
      errors: [],
      userName: "",
      password: "",
      serverMessage: "",
      hasServerError: false,
    };
  },
  methods: {
    hasError(key) {
      return this.errors.indexOf(key) != -1;
    },
    showServerMessage() {
      if (!this.serverMessage) {
        return "d-none";
      }

      return this.hasServerError
        ? "text-danger text-left"
        : "text-success text-left";
    },
    async onSubmit(e) {
      e.preventDefault();
      if (this.validateForm()) {
        const userDetails = {
          userName: this.userName,
          passKey: btoa(this.password),
        };

        const response = await axios.post(
          "https://localhost:44353/api/User/ValidateUser",
          userDetails
        );

        if (response.data.hasError) {
          this.hasServerError = response.data.hasError;
          this.serverMessage = response.data.errorMessage;
          return;
        }

        console.log(response.data);
      }
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
      return isValidFlag;
    },
  },
};
</script>

<style scoped></style>
