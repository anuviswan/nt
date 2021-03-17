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
import { computed, ref } from "vue";
import useValidator from "@/utils/inputValidators.js";
import { minLength, isEquals } from "@/utils/validators.js";
import { registerUser } from "@/api/user.js";
import router from "@/router";
export default {
  name: "Register",
  setup() {
    const userName = ref("");
    const password = ref("");
    const confirmPassword = ref("");
    const displayName = ref("");
    const errors = ref([]);
    const hasServerError = ref(false);
    const serverMessage = ref("");

    const onSubmit = async (e) => {
      e.preventDefault();
      errors.value = [];

      useValidator(userName.value, [minLength(3)], (e) =>
        errors.value.push({ "userName": e })
      );

      useValidator(displayName.value, [minLength(3)], (e) =>
        errors.value.push({ "displayName": e })
      );

      useValidator(password.value, [minLength(3)], (e) =>
        errors.value.push({ "password": e })
      );
      useValidator(
        confirmPassword.value,
        [minLength(3), isEquals(password.value, confirmPassword.value)],
        (e) => errors.value.push({ "confirmPassword": e })
      );

      if (errors.value.length == 0) {
        var response = await registerUser(
          userName.value,
          displayName.value,
          password.value
        );
        console.log(response);

        if (response.hasError) {
          hasServerError.value = true;
          serverMessage.value = response.error;
          return;
        }

        console.log("User authenticated and updated, redirecting now..");
        router.push("/Home");
      }
    };

    const hasError = (key) => {
      //const result = errors.value.indexOf(key) != -1;
      const result = errors.value.some((x) => key in x);
      return result;
    };

    const userNameError = computed(() => {
      if (hasError("userName")) {
        const result = errors.value.filter((x) => {
          if (x.userName) {
            return true;
          }
          return false;
        });
        return result[0].userName;
      }

      return [];
    });

    const passwordError = computed(() => {
      if (hasError("password")) {
        const result = errors.value.filter((x) => {
          if (x.password) {
            return true;
          }
          return false;
        });
        return result[0].password;
      }

      return [];
    });

    const confirmPasswordError = computed(() => {
      if (hasError("confirmPassword")) {
        const result = errors.value.filter((x) => {
          if (x.confirmPassword) {
            return true;
          }
          return false;
        });
        return result[0].confirmPassword;
      }

      return [];
    });

    return {
      userName,
      password,
      confirmPassword,
      onSubmit,
      errors,
      hasError,
      userNameError,
      passwordError,
      confirmPasswordError,
    };
  },
};
</script>

<style></style>
