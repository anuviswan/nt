<template>
    <div class="card card-block rounded shadow shadow-sm">
        <div class="card-header bg-primary text-light text-uppercase">
            <div class="card-title align-middle">
                <h5 class="mb-0">Sign Up</h5>
            </div>
        </div>
        <div class="card-body">
            <form class="form needs-validation" @submit.prevent="onSubmit">
                <div class="form-group">
                    <input type="text" v-model="formData.userName" v-bind:class="hasError('userName')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                        " placeholder="Username" />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.userName.$error">
                    <ValidationMessage :messages="v$.formData.userName.$errors.map((x:any) => x.$message)" v-bind:isError="true" />
                </div>
                <div class="form-group">
                    <input type="password" v-model="formData.password" v-bind:class="hasError('password')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                        " placeholder="Password" />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.password.$error">
                    <ValidationMessage :messages="v$.formData.password.$errors.map((x:any) => x.$message)" v-bind:isError="true" />
                </div>
                <div class="form-group">
                    <input type="password" v-model="formData.confirmPassword" v-bind:class="hasError('confirmPassword')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                        " placeholder="Confirm Password" />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.confirmPassword.$error">
                    <ValidationMessage :messages="v$.confirmPassword.$errors.map((x : any) => x.$message)" v-bind:isError="true" />
                </div>

                <div class="form-group">
                    <input type="submit" class="btn btn-block btn-primary" value="Submit" />
                </div>
            </form>
            <div>
                Already a member ?
                <router-link to="/">Sign in here</router-link>
            </div>
        </div>
    </div>
</template>
  
<script setup lang="ts">
import { ref, computed } from "vue";
import ValidationMessage from "@/components/generic/ValidationMessage.vue";
import { useVuelidate } from '@vuelidate/core'
import { required, minLength, sameAs, helpers } from '@vuelidate/validators'
import {userApiService} from "@/apiService/UserApiService"
import { RegisterUserRequest } from "@/apiService/userType";
//   import {registerUser} from "@/api/user.js"


interface IFormData {
    userName: string;
    password: string;
    confirmPassword: string;
}

const formData = ref<IFormData>({
    userName: '',
    password: '',
    confirmPassword: ''
});

const rules = computed(() => ({
    formData: {
        userName: {
            required: helpers.withMessage('Username cannot be empty', required),
            minLengthValue: helpers.withMessage('Username should minimum 4 characters', minLength(4))
        },
        password: { required: helpers.withMessage('Password cannot be empty', required), },
        confirmPassword: { sameAsPassword: helpers.withMessage('Password do not match', sameAs(formData.value.password)) }
    }

}))

function hasError(d:string):boolean{
    console.log(d)
    return false;
}

const v$ = useVuelidate(rules, { formData });

const onSubmit = async (): Promise<void> => {

    var validationResult = await v$.value.$validate();
    if (!validationResult) {
        console.log("Validation failed");
        return;
    }

    
    var response = await userApiService.registerUser({
        userName : formData.value.userName,
        password : formData.value.password,
    });
    
    //    await registerUser(userName.value,'',password.value);
}



</script>
  
<style></style>