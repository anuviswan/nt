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

              <div class="d-flex justify-content-left">
                <ValidationMessage
                  v-bind:messages="oldPasswordError"
                  v-bind:isError="true"
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

              <div class="d-flex justify-content-left">
                <ValidationMessage
                  v-bind:messages="newPasswordError"
                  v-bind:isError="true"
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

              <div class="d-flex justify-content-left">
                <ValidationMessage
                  v-bind:messages="confirmPasswordError"
                  v-bind:isError="true"
                />
              </div>

              <div class="form-group">
                <input
                  type="submit"
                  class="btn-primary"
                  value="Update Password"
                />
              </div>

              <div class="d-flex justify-content-left">
                <ValidationMessage
                  v-bind:messages="serverMessage"
                  v-bind:isError="hasServerError"
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

<script>
import { ref, computed } from "vue";
import { changePassword } from "@/api/user";
import { minLength, isEquals } from "@/utils/validators.js";
import useValidator from "@/utils/inputValidators.js";
import ValidationMessage from "@/components/generic/ValidationMessage";

export default {
  name: "ChangePassword",
  components: {
    ValidationMessage,
  },
  setup() {
    const serverMessage = ref([]);
    const hasServerError = ref(false);
    const oldPassword = ref("");
    const newPassword = ref("");
    const confirmPassword = ref("");
    const errors = ref([]);

    const onSubmit = async (e) => {
      e.preventDefault();
      errors.value = [];
      hasServerError.value = false;
      serverMessage.value = [];

      console.log(oldPassword.value);
      console.log(newPassword.value);
      console.log(confirmPassword.value);

      useValidator(oldPassword.value, [minLength(8)], (e) =>
        errors.value.push({ "oldPassword": e })
      );

      useValidator(newPassword.value, [minLength(8)], (e) =>
        errors.value.push({ "newPassword": e })
      );

      useValidator(
        confirmPassword.value,
        [minLength(8), isEquals(newPassword.value)],
        (e) => errors.value.push({ "confirmPassword": e })
      );

      if (errors.value.length == 0) {
        const response = await changePassword(
          oldPassword.value,
          newPassword.value
        );

        if (response.hasError) {
          hasServerError.value = true;
          serverMessage.value = response.error;
          return;
        }

        serverMessage.value.push("Password has been changed successfully.");
        console.log(serverMessage.value);
        console.log(response);
      }

      console.log(errors.value);
    };

    const hasError = (key) => {
      const result = errors.value.some((x) => key in x);
      return result;
    };

    const oldPasswordError = computed(() => {
      if (hasError("oldPassword")) {
        const result = errors.value.filter((x) => {
          if (x.oldPassword) {
            return true;
          }
          return false;
        });
        return result[0].oldPassword;
      }

      return [];
    });

    const newPasswordError = computed(() => {
      if (hasError("newPassword")) {
        const result = errors.value.filter((x) => {
          if (x.newPassword) {
            return true;
          }
          return false;
        });
        return result[0].newPassword;
      }

      return [];
    });

    const confirmPasswordError = computed(() => {
      if (hasError("confirmPassword")) {
        const result = errors.value.filter((x) => {
          if (x.confirmPassword) {
            return true;
          }
          return false;
        });
        return result[0].confirmPassword;
      }

      return [];
    });

    const showServerMessage = () => {
      if (!serverMessage.value) {
        return "d-none justify-content-center";
      }

      return hasServerError.value
        ? "text-danger justify-content-center"
        : "text-success justify-content-center";
    };

    return {
      serverMessage,
      oldPassword,
      newPassword,
      confirmPassword,
      onSubmit,
      hasError,
      hasServerError,
      showServerMessage,
      newPasswordError,
      oldPasswordError,
      confirmPasswordError,
    };
  },
};
</script>

<style></style>
