<template>
    <div class="container-fluid">
        <div class="card card-block rounded shadow shadow-sm">
        <div class="card-header bg-primary text-light text-uppercase">
            <div class="card-title align-middle">
                <h5 class="mb-0">Edit User</h5>
            </div>
        </div>
        <div class="card-body">
            <form class="form needs-validation" @submit.prevent="onSubmit">
                <div class="form-group">
                    <input type="text" v-model="formData.userName" class="form-control block" placeholder="Username" />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.userName.$error">
                    <ValidationMessage :messages="v$.formData.userName.$errors.map((x: any) => x.$message)"
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
        </div>
    </div>
    </div>
</template>
<script setup lang="ts">
import {ref, computed} from 'vue'
import { required, minLength, sameAs, helpers } from '@vuelidate/validators'
import { useVuelidate } from '@vuelidate/core'

interface IFormData{
    userName : string,
    displayName : string,
    bio : string,
}

const formData = ref<IFormData>({
    userName : '',
    displayName : '',
    bio : ''
});
const $externalResults = ref({});
const serverMessage = ref<string>('');

const rules = computed(() => ({
    formData: {
        userName: {
            required: helpers.withMessage('Username cannot be empty', required),
            minLengthValue: helpers.withMessage('Username should minimum 4 characters', minLength(4))
        },
        displayName :{
            required: helpers.withMessage('DisplayName cannot be empty', required),
            minLengthValue: helpers.withMessage('DisplayName should minimum 6 characters', minLength(6))
        },
    },
    serverMessage: {}

}))


const v$ = useVuelidate(rules, { formData, serverMessage }, { $externalResults });

const onSubmit = async () : Promise<void>=>{

}

</script>
<style scoped>
</style>