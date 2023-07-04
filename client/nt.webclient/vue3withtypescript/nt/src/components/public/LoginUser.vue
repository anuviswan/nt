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
          <input
            type="text"
            v-model="userName"
            placeholder="Username"
            class="form-control block"
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
            placeholder="Password"
            class="form-control block"
          />
        </div>
        <div class="d-flex justify-content-left" v-if="v$.password.$error">
        <ValidationMessage   :messages="v$.password.$errors.map(x=>x.$message)"
            v-bind:isError="true"
          />
        </div>
        <div class="form-group">
          <input
            type="submit"
            class="btn btn-block btn-primary"
            value="Submit"
            v-bind:disabled="clientValidationSucceeded == false"
          />
        </div>
        <div class="d-flex justify-content-left">
          <ValidationMessage
            v-bind:messages="serverMessage"
            v-bind:isError="hasServerError"
          />
        </div>
      </form>
      <div>
        Not a member ?
        <router-link to="/register">Sign up here</router-link>
      </div>
      <div>
        <!-- <ValidationMessage v-bind="v$.value.$externalResults.$errors.map(x=>x.$message)"/> -->
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
const userName : string = '';
const password : string - '';

</script>