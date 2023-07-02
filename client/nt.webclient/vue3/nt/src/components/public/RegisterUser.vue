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
            v-bind:class="
              hasError('userName')
                ? 'form-control block is-invalid'
                : 'form-control block'
            "
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
            v-bind:class="
              hasError('password')
                ? 'form-control block is-invalid'
                : 'form-control block'
            "
            placeholder="Password"
          />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.password.$error">
          <ValidationMessage v-if="v$.password.$error.required"
            messages="Password is required"
            v-bind:isError="true"
          />
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
        <div class="d-flex justify-content-left">
          <ValidationMessage
            v-bind:messages="confirmPasswordError"
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
        <!-- <div class="d-flex justify-content-left">
          <ValidationMessage
            v-bind:messages="serverMessage"
            v-bind:isError="hasServerError"
          />
        </div> -->
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
// import { registerUser } from "@/api/user.js";
import ValidationMessage from "@/components/generic/ValidationMessage";
import { useVuelidate } from '@vuelidate/core'
import { required, minLength,helpers  } from '@vuelidate/validators'


const userName = ref("");
const password = ref("");
const confirmPassword = ref("");


const rules = computed(()=>({
  userName : {
                required:helpers.withMessage('Username cannot be empty', required), 
                minLengthValue: helpers.withMessage('Username should minimum 4 characters',minLength(4)) 
             },
  password : {required:helpers.withMessage('Password cannot be empty', required),  },
  confirmPassword : { required}
}))

const hasError = (d)=> {
  console.log(d);
  return true;
}

const v$ = useVuelidate(rules,{userName,password,confirmPassword});

const onSubmit = async () => {
 await v$.value.$validate();
  console.log(v$);
console.log(v$.value.$error);
  // var validationResult = await v$.$validate();
  // if (!validationResult) {
  //       console.log("Validation failed");
  //       return;
  //     }
    //   await registerUser(
    //   formState.userName.value,
    //   '',
    //   formState.password.value
    // );
}



</script>

<style></style>
