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
                    <input type="text" v-model="formData.userName" class="form-control block"
                         placeholder="Username" @blur="v$.formData.userName.$touch()"/>
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.userName.$error">
                    <ValidationMessage :messages="v$.formData.userName.$errors.map((x: any) => x.$message)"
                        v-bind:isError="true" />
                </div>
                <div class="form-group">
                    <input type="password" v-model="formData.password" class="form-control block" 
                    placeholder="Password"  @blur="v$.formData.password.$touch()"/>
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.password.$error">
                    <ValidationMessage :messages="v$.formData.password.$errors.map((x: any) => x.$message)"
                        v-bind:isError="true" />
                </div>
                <div class="form-group">
                    <input type="password" v-model="formData.confirmPassword" class="form-control block" 
                    placeholder="Confirm Password"  @blur="v$.formData.confirmPassword.$touch()"/>
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.confirmPassword.$error">
                    <ValidationMessage :messages="v$.formData.confirmPassword.$errors.map((x: any) => x.$message)"
                        v-bind:isError="true" />
                </div>

                <div class="form-group">
                    <input type="submit" class="btn btn-block btn-primary" value="Submit" />
                </div>

                <div class="d-flex justify-content-left" v-if="v$.serverMessage.$error">
                    <ValidationMessage :messages="v$.serverMessage.$errors.map((x: any) => x.$message)"
                        v-bind:isError="true" />
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
import { userApiService } from "@/apiService/UserApiService"
import { RegisterUserRequest } from "@/apiService/userType";
import { IRegisterUserRequest } from "@/types/ApiRequestResponseTypes";
//   import {registerUser} from "@/api/user.js"


interface IFormData {
    userName: string;
    password: string;
    confirmPassword: string;
}

const formData = ref<IFormData>({
    userName: '',
    password: '',
    confirmPassword: '',
});

const $externalResults = ref({});
const serverMessage = ref<string>('');

const rules = computed(() => ({
    formData: {
        userName: {
            required: helpers.withMessage('Username cannot be empty', required),
            minLengthValue: helpers.withMessage('Username should minimum 6 characters', minLength(6))
        },
        password: { required: helpers.withMessage('Password cannot be empty', required), },
        confirmPassword: { sameAsPassword: helpers.withMessage('Password do not match', sameAs(formData.value.password)) }
    },
    serverMessage: {}

}))


const v$ = useVuelidate(rules, { formData, serverMessage }, { $externalResults });

const onSubmit = async (): Promise<void> => {
    v$.value.$clearExternalResults();
    var validationResult = await v$.value.$validate();

    if (!validationResult) {
        console.log("Validation failed");
        return;
    }

    var response = await userApiService.registerUser({
        userName: formData.value.userName,
        passKey: formData.value.password,
    } as IRegisterUserRequest);

    if (response.status != 200) {
        console.log("Register failed")

        if (response.errors != null) {
            const errors = {
                formData: {
                    password: response.errors.Password,
                    confirmPassword: response.errors.ConfirmPassword,
                    userName: response.errors.UserName
                }
            };

            $externalResults.value = errors;
        }
    }
    else {
        console.log("Register succeeded")
    }


    //    await registerUser(userName.value,'',password.value);
}



</script>
  
<style></style>