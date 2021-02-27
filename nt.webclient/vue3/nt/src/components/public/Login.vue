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
          <div class="d-flex justify-content-center">
            <ValidationMessage
              v-bind:messages="getError('userName')"
              v-bind:isError="true"
            />
          </div>
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
        Not a member ?
        <router-link to="/register">Sign up here</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { ref, reactive } from "vue";
import ValidationMessage from "@/components/generic/ValidationMessage";
import inputValidator from "@/utils/inputValidators.js";
import { minLength } from "@/utils/validators.js";

export default {
  name: "Login",
  components: {
    ValidationMessage,
  },
  setup() {
    const userName = ref("");
    const password = ref("");
    const serverMessage = ref("");
    const hasServerError = ref(false);
    let errors = reactive([]);

    const hasError = (key) => {
      return errors.indexOf(key) != -1;
    };

    const getError = (key) => {
      console.log("getError " + key + errors[key]);
      return errors[key];
    };

    const onSubmit = (e) => {
      e.preventDefault();

      console.log("validation 1");
      inputValidator(
        userName.value,
        [minLength(3)],
        (val) => (errors.value = [...errors, { "userName": val }])
      );
      console.log("validation 2");
      inputValidator(
        password.value,
        [minLength(3)],
        (val) => (errors.value = [...errors, { "password": val }])
      );

      console.log(errors.value);
    };

    return {
      userName,
      password,
      hasError,
      serverMessage,
      hasServerError,
      onSubmit,
      errors,
      getError,
    };
  },
};
</script>

<style></style>
