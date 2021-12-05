<template>
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
          <input
            type="submit"
            class="btn btn-block btn-primary"
            value="Submit"
          />
        </div>
        <div class="d-flex justify-content-center">
          <ValidationMessage
            v-bind:messages="serverMessage"
            v-bind:isError="hasServerError"
          />
        </div>
      </form>
      <div>
        Already a member ?
        <router-link to="/">Sign in here</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { registerUser } from "../../api/user";
import ValidationMessage from "../../components/generic/ValidationMessage";
export default {
  name: "Signup",
  components: {
    ValidationMessage,
  },
  data() {
    return {
      errors: [],
      userName: "",
      password: "",
      displayName: "",
      confirmPassword: "",
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

      if (!this.validateForm()) {
        // has Error
        console.log(this.userName);
        return;
      }

      this.hasServerError = false;
      this.serverMessage = [];

      const response = await registerUser(
        this.userName,
        this.displayName,
        this.password
      );

      if (response.hasError) {
        this.hasServerError = true;
        this.serverMessage = response.error;
        return;
      }

      this.serverMessage.push("User registered successfully.");
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
        this.errors.push("confirmPassword");
        isValidFlag = false;
      }
      return isValidFlag;
    },
  },
};
</script>

<style></style>
