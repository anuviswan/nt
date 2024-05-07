<!-- <template>
    <div class="container-fluid">
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
                <div class="d-flex justify-content-left" v-if="v$.formData.userName.$error">
                    <ValidationMessage :messages="v$.formData.userName.$errors.map((x: any) => x.$message)"
                        v-bind:isError="true" />
                </div>
                <div class="form-group">
                    <input type="password" v-model="formData.password" v-bind:class="hasError('password')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                        " placeholder="Password" />
                </div>
                <div class="d-flex justify-content-left" v-if="v$.formData.password.$error">
                    <ValidationMessage :messages="v$.formData.password.$errors.map((x: any) => x.$message)"
                        v-bind:isError="true" />
                </div>
                <div class="form-group">
                    <input type="password" v-model="formData.confirmPassword" v-bind:class="hasError('confirmPassword')
                        ? 'form-control block is-invalid'
                        : 'form-control block'
                        " placeholder="Confirm Password" />
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
    </div>
</template>
<script setup lang="ts">
import {ref} from 'vue'

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

const onSubmit = async () : Promise<void>=>{

}

</script>
<style scoped>
</style> -->