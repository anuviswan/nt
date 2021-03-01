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
          <small>UserName cannot be empty</small>
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
import { ref } from "vue";
import ValidationMessage from "@/components/generic/ValidationMessage";
import useValidator from "@/utils/inputValidators.js";
import { minLength } from "@/utils/validators.js";
import { validateUser } from "@/api/user.js";
import { useStore } from "vuex";
import router from "@/router";

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
    let errors = ref([]);
    const store = useStore();

    const onSubmit = async (e) => {
      e.preventDefault();
      errors.value = [];
      if (!useValidator(userName.value, [minLength(1)])) {
        errors.value.push("userName");
      }
      if (!useValidator(password.value, [minLength(1)])) {
        errors.value.push("password");
      }

      console.log(errors.value);
      if (errors.value.length == 0) {
        var response = await validateUser(userName.value, password.value);
        console.log(response);

        if (response.hasError) {
          hasServerError.value = true;
          serverMessage.value = response.error;
          return;
        }

        store.dispatch("updateCurrentUser", {
          userName: response.data.userName,
          displayName: response.data.displayName,
          bio: response.data.bio,
          token: response.data.token,
        });

        console.log("User authenticated and updated, redirecting now..");
        router.push("/p/dashboard");
      }
    };
    const hasError = (key) => {
      const result = errors.value.indexOf(key) != -1;
      return result;
    };

    const getError = (key) => {
      return errors[key];
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
