<template>
  <div class="card card-block rounded shadow shadow-sm">
    <div class="card-header bg-primary text-light text-uppercase">
      <div class="card-title align-middle">
        <h5 class="mb-0">Sign In</h5>
      </div>
    </div>
    <div class="card-body">
      <form class="form needs-validation" @submit.prevent="onSubmit">
        <div class="form-group">
          <input
            type="text"
            v-model="userName"
            placeholder="Username"
            class="form-control block"
          />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.userName.$error">
        <ValidationMessage   :messages="v$.userName.$errors.map(x=>x.$message)"
            v-bind:isError="true"
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
        <div class="d-flex justify-content-left" v-if="v$.password.$error">
        <ValidationMessage   :messages="v$.password.$errors.map(x=>x.$message)"
            v-bind:isError="true"
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
      <div>
        <!-- <ValidationMessage v-bind="v$.value.$externalResults.$errors.map(x=>x.$message)"/> -->
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from "vue";
import ValidationMessage from "@/components/generic/ValidationMessage";
import { useStore } from "vuex";
import { useVuelidate } from '@vuelidate/core'
import { required, helpers  } from '@vuelidate/validators'
import {validateUser} from "@/api/user.js"
import router from "@/router";

const userName = ref("");
const password = ref("");
const store = useStore();
const $externalResults = ref({})


const rules = computed(()=>({
  userName : {
                required:helpers.withMessage('Username cannot be empty', required), 
             },
  password : {
              required:helpers.withMessage('Password cannot be empty', required),  
            },
}));

const v$ = useVuelidate(rules,{userName,password},{ $externalResults });

const onSubmit = async () => {

  v$.value.$clearExternalResults();
   var validationResult = await v$.value.$validate();

   if(!validationResult){
    console.log("Validation failed");
    return;
   }
  var response = await validateUser(userName.value, password.value);
  console.log(response);

  if (response.hasError) {
    if (response.errorCode == 401) {
       const errors = {
        userName : ['Invalid Username or password']
       };

       console.log(errors);
       $externalResults.value = errors;
       console.log($externalResults);
      return;
    }
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
</script>

<style></style>
