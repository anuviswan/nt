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
        <div class="d-flex justify-content-left">
          <ValidationMessage
            v-bind:messages="userNameError"
            v-bind:isError="true"
          />
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

        <div class="d-flex justify-content-left">
          <ValidationMessage
            v-bind:messages="passwordError"
            v-bind:isError="true"
          />
        </div>

        <div class="form-group">
          <input
            type="submit"
            class="btn btn-block btn-primary"
            value="Submit"
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
import { computed, ref } from "vue";
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

      useValidator(userName.value, [minLength(3)], (e) =>
        errors.value.push({ "userName": e })
      );
      useValidator(password.value, [minLength(3)], (e) =>
        errors.value.push({ "password": e })
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

    return {
      userName,
      password,
      hasError,
      serverMessage,
      hasServerError,
      onSubmit,
      errors,
      userNameError,
      passwordError,
    };
  },
};
</script>

<style></style>
