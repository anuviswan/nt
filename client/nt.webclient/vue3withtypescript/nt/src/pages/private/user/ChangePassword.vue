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
                  <input
                    type="password"
                    placeholder="Old Password"
                    v-model="formData.oldPassword"
                    name="oldPassword"
                  />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.oldPassword.$error">
                  <ValidationMessage :messages="v$.formData.oldPassword.$errors.map((x: any) => x.$message)" v-bind:isError="true" />
                </div>
  
                <div class="form-group">
                  <input
                    type="text"
                    placeholder="New Password"
                    name="newPassword"
                    v-model="formData.newPassword"
                  />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.newPassword.$error">
                  <ValidationMessage :messages="v$.formData.newPassword.$errors.map((x: any) => x.$message)" v-bind:isError="true" />
                </div>
  
                <div class="form-group">
                  <input
                    type="text"
                    name="confirmPassword"
                    placeholder="Confirm Password"
                    v-model="formData.confirmPassword"
                  />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.confirmPassword.$error">
                  <ValidationMessage :messages="v$.formData.confirmPassword.$errors.map((x: any) => x.$message)" v-bind:isError="true" />
                </div>
  
                <div class="form-group">
                  <input
                    type="submit"
                    class="btn-primary"
                    value="Update Password"
                  />
                </div>
                <div class="d-flex justify-content-left" v-if="serverMessage">
                  <ValidationMessage :messages="serverMessage" v-bind:isError="true" />
                </div>
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