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
            placeholder="Username"
            class="form-control block"
          />
        </div>
        <div class="form-group">
          <input
            type="password"
            v-model="password"
            placeholder="Password"
            class="form-control block"
          />
        </div>

        <div class="form-group">
          <input
            type="submit"
            class="btn btn-block btn-primary"
            value="Submit"
            v-bind:disabled="clientValidationSucceeded == false"
          />
        </div>
        <div class="d-flex justify-content-left">
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
    const clientValidationSucceeded = ref(false);
    let errors = ref([]);
    const store = useStore();

    const onSubmit = async (e) => {
      e.preventDefault();
      errors.value = [];

      useValidator(
        userName.value,
        [minLength(1)],
        () => {},
        () => {
          clientValidationSucceeded.value = false;
        }
      );
      useValidator(
        password.value,
        [minLength(1)],
        () => {},
        () => {
          clientValidationSucceeded.value = false;
        }
      );

      if (errors.value.length == 0) {
        var response = await validateUser(userName.value, password.value);
        console.log(response);

        if (response.hasError) {
          hasServerError.value = true;
          serverMessage.value = response.error;
          return;
        }

        const currentUser = {
          userName: response.data.userName,
          displayName: response.data.displayName,
          bio: response.data.bio,
          token: response.data.token,
        };

        console.log(currentUser);

        store.dispatch("updateCurrentUser", currentUser);

        console.log("User authenticated and updated, redirecting now..");
        router.push("/p/dashboard");
      }
    };

    return {
      userName,
      password,
      serverMessage,
      hasServerError,
      onSubmit,
      errors,
      clientValidationSucceeded,
    };
  },
};
</script>

<style></style>
