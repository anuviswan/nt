<template>
  <div class="card card-block rounded shadow shadow-sm" >
    <div class="card-header bg-primary text-light text-uppercase">
      <div class="card-title align-middle">
        <h5 class="mb-0">Sign Up</h5>
      </div>
    </div>
    <div class="card-body">
      <form class="form needs-validation" @submit.prevent="onSubmit">
        <div class="form-group">
          <input
            type="text"
            v-model="userName"
            class="form-control block"
            placeholder="Username"
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
            class="form-control block"
            placeholder="Password"
          />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.password.$error">
        <ValidationMessage   :messages="v$.password.$errors.map(x=>x.$message)"
            v-bind:isError="true"
          />
        </div>
        <div class="form-group">
          <input
            type="password"
            v-model="confirmPassword"
            class="form-control block"
            placeholder="Confirm Password"
          />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.confirmPassword.$error">
        <ValidationMessage   :messages="v$.confirmPassword.$errors.map(x=>x.$message)"
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
      </form>
      <div>
        Already a member ?
        <router-link to="/">Sign in here</router-link>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from "vue";
import ValidationMessage from "@/components/generic/ValidationMessage";
import { useVuelidate } from '@vuelidate/core'
import { required, minLength,sameAs, helpers  } from '@vuelidate/validators'
import {registerUser} from "@/api/user.js"

const userName = ref("");
const password = ref("");
const confirmPassword = ref("");
const $externalResults = ref({})


const rules = computed(()=>({
  userName : {
                required:helpers.withMessage('Username cannot be empty', required), 
                minLengthValue: helpers.withMessage('Username should minimum 4 characters',minLength(4)) 
             },
  password : 
            {
              required:helpers.withMessage('Password cannot be empty', required),  
              minLengthValue: helpers.withMessage('Password should minimum 4 characters',minLength(4)) 
            },
  confirmPassword : { sameAsPassword: helpers.withMessage('Password do not match', sameAs(password))}
}))

const v$ = useVuelidate(rules,{userName,password,confirmPassword},{ $externalResults});

const onSubmit = async () => {
 
  v$.value.$clearExternalResults();
  var validationResult = await v$.value.$validate();
  if (!validationResult) {
        console.log("Validation failed");
        return;
      }
      const response = await registerUser(userName.value,'',password.value);

      if(response.hasError){
        // error
      }
      else{
        const errors = {
        userName : ['user created successfully']
       };
        $externalResults.value = errors;
      }


}



</script>

<style></style>
