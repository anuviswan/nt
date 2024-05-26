<template>
    <div class="container-fluid">
        <div class="row row-cols-2">
            <div class="col col-4 col-xl-2 align-self-start">
                <div class="card rounded shadow shadow-sm">
                    <div class="card-body bg-light text-uppercase">
                        <div class="card-title text-left">
                            <h5 class="mb-0" align-middle>Photo</h5>
                        </div>
                    </div>
                    <div class="card-body">
                        <div>
                            <AvataarCard />
                        </div>
                    </div>
                </div>
            </div>

            <div class="col col-8 col-xl-8 align-self-start">
                <div class="card rounded shadow shadow-sm">
                    <div class="card-body bg-light text-uppercase">
                        <div class="card-title text-left">
                            <h5 class="mb-0" align-middle>Edit User</h5>
                        </div>
                    </div>
                    <div class="card-body">
                        <form class="form needs-validation" @submit.prevent="onSubmit">
                            <div class="form-group text-left">
                                <label>UserName</label>
                                <input type="text" v-model="formData.userName" class="form-control block"
                                    placeholder="Username" readonly />
                            </div>
                            <div class="form-group text-left">
                                <label>Display Name</label>
                                <input type="text" v-model="formData.displayName" class="form-control block"
                                    placeholder="Display Name" />
                            </div>
                            <div class="form-group text-left">
                                <label>About yourself</label>
                                <input type="text" v-model="formData.bio" class="form-control block"
                                    placeholder="Hello !" />
                            </div>

                            <div class="form-group">
                                <input type="submit" class="btn btn-block btn-primary" value="Submit" />
                            </div>

                            <div class="d-flex justify-content-left" v-if="v$.serverMessage.$error">
                                <ValidationMessage :messages="v$.serverMessage.$errors.map((x: any) => x.$message)"
                                    v-bind:isError="true" />
                            </div>
                        </form>

                    </div>
                </div>
            </div>
        </div>

    </div>
</template>
<script setup lang="ts">
import { ref, computed } from 'vue'
import { required, minLength, helpers } from '@vuelidate/validators'
import { useVuelidate } from '@vuelidate/core'
import AvataarCard from '@/components/private/user/AvataarCard.vue'
import {useUserStore } from '@/stores/userStore';
import { userApiService } from '@/apiService/UserApiService';

const store = useUserStore();

interface IFormData {
    userName: string,
    displayName: string,
    bio: string,
}

const formData = ref<IFormData>({
    userName: store.UserName,
    displayName: store.DisplayName,
    bio: store.Bio
});
const $externalResults = ref({});
const serverMessage = ref<string>('');

const rules = computed(() => ({
    formData: {
        userName: {
            required: helpers.withMessage('Username cannot be empty', required),
            minLengthValue: helpers.withMessage('Username should minimum 6 characters', minLength(6))
        },
    },
    serverMessage: {}

}))


const v$ = useVuelidate(rules, { formData, serverMessage }, { $externalResults });

const onSubmit = async (): Promise<void> => {

    console.log('Submiting changes with new values [displayName:' + formData.value.displayName +'bio:'+ formData.value.bio + ']');

    v$.value.$clearExternalResults();
    var validationResult = await v$.value.$validate();

    if (!validationResult) {
        console.log("Validation failed");
        return;
    }

    console.log("validation succeeded")

    var response = await userApiService.updateUser({
      displayName : formData.value.displayName,
      bio :  formData.value.bio
    });

    console.log(response);
    console.log(response.status);

    if (response.status != 200) {
        console.log("User Profile updattion failed")

        if (response.errors != null) {
            const errors = {
                formData: {
                    displayName: response.errors.displayName,
                    bio: response.errors.bio,
                }
            };

            $externalResults.value = errors;
        }
    }
    else {
        console.log("User Profile Updated successfully");
        store.DisplayName = response.displayName;
        store.Bio = response.bio;
    }

    
}


</script>
<style scoped></style>