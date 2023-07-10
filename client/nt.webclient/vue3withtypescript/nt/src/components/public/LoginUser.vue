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
          <input type="text" v-model="formData.userName" placeholder="Username" class="form-control block" />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.formData.userName.$error">
          <ValidationMessage :messages="v$.formData.userName.$errors.map((x: any) => x.$message)" v-bind:isError="true" />
        </div>
        <div class="form-group">
          <input type="password" v-model="formData.password" placeholder="Password" class="form-control block" />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.formData.password.$error">
          <ValidationMessage :messages="v$.formData.password.$errors.map((x: any) => x.$message)" v-bind:isError="true" />
        </div>
        <div class="form-group">
          <input type="submit" class="btn btn-block btn-primary" value="Submit" />
        </div>
        <div class="d-flex justify-content-left" v-if="serverMessage">
        <ValidationMessage :messages="serverMessage" v-bind:isError="true" />
      </div>
        <!-- <div class="d-flex justify-content-left">
          <ValidationMessage
            v-bind:messages="serverMessage"
            v-bind:isError="hasServerError"
          />
        </div> -->
      </form>
      <div>
        Not a member ?
        <router-link to="/register">Sign up here</router-link>
      </div>

    </div>
  </div>
</template>

<script setup lang="ts">
import { userApiService } from "@/apiService/UserApiService";
import { IValidateUserRequest } from "@/types"
import useVuelidate from "@vuelidate/core";
import { helpers, required } from "@vuelidate/validators";
import { computed, ref } from "vue";
import ValidationMessage from "@/components/generic/ValidationMessage.vue";
import LoggedInUser from "@/types/StoreTypes";
import useUserStore from "@/stores/userStore"
interface IFormData {
  userName: string;
  password: string;
}

const formData = ref<IFormData>({
  userName: '',
  password: ''
});

const userStoreInstance = userStore();

const $externalResults = ref({});
const serverMessage = ref<string[]>([]);

const rules = computed(() => ({
  formData: {
    userName: {
      required: helpers.withMessage('Username cannot be empty', required),
    },
    password: { required: helpers.withMessage('Password cannot be empty', required), },
  },

}))

const v$ = useVuelidate(rules, { formData }, { $externalResults });

const onSubmit = async (): Promise<void> => {

  v$.value.$clearExternalResults();
  var validationResult = await v$.value.$validate();

  if (!validationResult) {
    console.log("Validation failed");
    return;
  }

  // Validation succeeded, raise Api call

  var userInfo: IValidateUserRequest = {
    userName: formData.value.userName,
    password: formData.value.password
  };

  var response = await userApiService.validateUser(userInfo);

  // Validate response from Api
  if (response.hasError) {
    console.log("Login failed")

    if (response.status == 401) {
      serverMessage.value = ['Invalid Username or Password'];
      console.log(v$);
    }
    else if (response.errors != null) {
      const errors = {
        formData: {
          password: response.errors.Password,
          userName: response.errors.UserName
        }
      };

      console.log(errors)
      $externalResults.value = errors;

      console.log(v$);
    }
  }
  else 
  { 
    console.log('succeeeded'); 
  // Store in VueStore
    const loggedInUser : LoggedInUser = { 
      userName : response.data.userName,
      displayName : response.data.displayName,
      bio : response.data.bio,
      token : response.data.token      
    };

    userStoreInstance.SaveUser(loggedInUser);
  }

}


function userStore() {
  throw new Error("Function not implemented.");
}
</script>