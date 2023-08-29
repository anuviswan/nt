<template>
    <div class="container-fluid">
      <div class="row align-items-center justify-content-center">
        <div class="col-sm-8 col-md-6 col-lg-6">
          <div class="card">
            <div class="card-header font-weight-bold text-uppercase">
              Change Password
            </div>
            <div class="card-body">
              <form class="form needs-validation" v-on:submit="onSubmit">
                <div class="form-group">
                  <label for="oldPassword">Old Password</label>
                  <input
                    type="password"
                    v-model="formData.oldPassword"
                    name="oldPassword"
                  />
                </div>
  
                <div class="form-group">
                  <label for="newPassword">New Password</label>
                  <input
                    type="text"
                    name="newPassword"
                    v-model="formData.newPassword"
                  />
                </div>
  
                <div class="form-group">
                  <label for="confirmPassword">Confirm Password</label>
                  <input
                    type="text"
                    name="confirmPassword"
                    v-model="formData.confirmPassword"
                  />
                </div>
  
                <div class="form-group">
                  <input
                    type="submit"
                    class="btn-primary"
                    value="Update Password"
                  />
                </div>
                <!-- <div class="form-group">
                  <div v-bind:class="showServerMessage()">
                    <small>{{ this.serverMessage }}</small>
                  </div>
                </div> -->
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>

<script lang="ts" setup>
import { useVuelidate } from '@vuelidate/core'
import { required, minLength, sameAs, helpers } from '@vuelidate/validators'
import { ref, computed } from "vue";

interface IFormData{
    oldPassword : string,
    newPassword : string,
    confirmPassword : string
}

const formData = ref<IFormData>({
    oldPassword : '',
    newPassword : '',
    confirmPassword : ''
})
const $externalResults = ref({});
const serverMessage = ref<string>('');

const rules = computed(() => ({
    formData: {
        oldPassword: {
            required: helpers.withMessage('Old Password cannot be empty', required)
        },
        newPassword: 
        { 
          required: helpers.withMessage('New Password cannot be empty', required), 
          minLengthValue: helpers.withMessage('Password should minimum 8 characters', minLength(8))
        },
        confirmPassword: { sameAsPassword: helpers.withMessage('Password do not match', sameAs(formData.value.newPassword)) }
    },
    serverMessage: {}

}))

const v$ = useVuelidate(rules, { formData, serverMessage }, { $externalResults });

const onSubmit = async ()=>{


    v$.value.$clearExternalResults();
    var validationResult = await v$.value.$validate();

    if (!validationResult) {
        console.log("Validation failed");
        return;
    }

    // Validate Old Password


    // Change new Password
    return false;
}


</script>
<style scoped>

</style>