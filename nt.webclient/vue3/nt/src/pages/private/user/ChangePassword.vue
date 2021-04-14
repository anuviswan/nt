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
                  v-model="oldPassword"
                  name="oldPassword"
                  v-bind:class="
                    hasError('oldPassword')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>

              <div class="form-group">
                <label for="newPassword">New Password</label>
                <input
                  type="text"
                  name="newPassword"
                  v-model="newPassword"
                  v-bind:class="
                    hasError('newPassword')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>

              <div class="form-group">
                <label for="confirmPassword">Confirm Password</label>
                <input
                  type="text"
                  name="confirmPassword"
                  v-model="confirmPassword"
                  v-bind:class="
                    hasError('confirmPassword')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>

              <div class="form-group">
                <input
                  type="submit"
                  class="btn-primary"
                  value="Update Password"
                />
              </div>
              <div class="form-group">
                <div v-bind:class="showServerMessage()">
                  <small>{{ this.serverMessage }}</small>
                </div>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
export default {
  setup() {
    const serverMessage = ref("");
    const oldPassword = ref("");
    const newPassword = ref("");
    const confirmPassword = ref("");

    return {
      serverMessage,
      oldPassword,
      newPassword,
      confirmPassword,
    };
  },
};
</script>

<style></style>
