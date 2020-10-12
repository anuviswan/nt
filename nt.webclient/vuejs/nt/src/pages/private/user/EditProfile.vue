<template>
  <div class="container-fluid">
    <div class="row">
      <div class="col-sm-2 col-md-2 col-lg-2">
        <EditUserMenu />
      </div>
      <div class="col-sm-8 col-md-8 col-lg-10">
        <div class="card">
          <div class="card-header font-weight-bold text-uppercase">
            Edit User Profile
          </div>
          <div class="card-body">
            <form class="form needs-validation" v-on:submit="onSubmit">
              <div class="form-group">
                <label for="userName">User Name</label>
                <input
                  type="text"
                  v-bind:placeholder="userName"
                  name="userName"
                  readOnly
                />
              </div>
              <div class="form-group">
                <label for="displayName">Display Name</label>
                <input
                  type="text"
                  v-model="displayName"
                  name="displayName"
                  v-bind:class="
                    hasError('displayName')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <label for="bio">Bio</label>
                <textarea
                  v-model="bio"
                  name="bio"
                  v-bind:class="
                    hasError('bio')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <input type="submit" value="Update" />
              </div>
                      <div v-bind:class="showServerMessage()">
          <ul>
            <li v-for="(error, index) in serverMessage" :key="index">
              <small>{{ error }}</small>
            </li>
          </ul>
         
        </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import EditUserMenu from "../../../components/user/EditUserMenu";
import axios from "axios";
import { mapGetters, mapActions } from "vuex";
export default {
  name: "EditProfile",
  components: {
    EditUserMenu,
  },
  data() {
    return {
      authToken: "",
      userName: "sampleUserName",
      displayName: "sample Display Name",
      bio: "sample Bio",
      errors: [],
      serverMessage: [],
      hasServerError: false,
    };
  },
  created: function () {
    console.log(this.currentUser);
    this.userName = this.$route.params.userid;
    this.authToken = this.currentUser.token;
    this.displayName = this.currentUser.displayName;
    this.bio = this.currentUser.bio;
  },
  computed: mapGetters(["currentUser"]),
  methods: {
    ...mapActions(["updateCurrentUser"]),
    async onSubmit(e) {
      e.preventDefault();
      if (this.validateForm()) {
        console.log("User Profile Update:Submiting data....");

        const headers = {
          "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
          "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
          "Content-Type": "application/json", // this shows the expected content type
          Authorization: `Bearer ${this.authToken}`,
        };

        const updatedUserRecord = {
          displayName: this.displayName,
          bio: this.bio,
        };

        try{
          var response = await axios.post(
          "https://localhost:44353/api/User/UpdateUser",
          updatedUserRecord,
          { headers: headers }
        );
        console.log(response);

        if (response.data.hasError) {
          this.hasServerError = response.data.hasError;
          this.serverMessage = response.data.errorMessage;
          return;
        }

        this.updateCurrentUser({
          userName: this.userName,
          displayName: response.data.displayName,
          bio: response.data.bio,
          token: this.authToken,
        });

        this.serverMessage = "User Profile updated successfully";
        }
         catch (error) {
           console.log("An Exception has occurred");
           console.log(error.response.data.errors);

           for(var i=0;i<error.response.data.errors.length;i++){
              this.serverMessage.concat(error.response.data.errors[i]);
           }
           console.log("Following is server message");
           console.log(this.serverMessage);
          //this.serverMessage = error.response.data.errors.NewPassword.shift();
          this.hasServerError = true;
          return;
        }
        
      }
      console.log(this.errors);
    },
    hasError(key) {
      return this.errors.indexOf(key) != -1;
    },
    validateForm() {
      let isValidFlag = true;
      this.errors = [];

      if (!this.displayName) {
        this.errors.push("displayName");
        isValidFlag = false;
      }

      if (!this.bio) {
        this.errors.push("bio");
        isValidFlag = false;
      }

      return isValidFlag;
    },
    showServerMessage() {
      if (!this.serverMessage.length) {
        return "d-none";
      }

      return this.hasServerError
        ? "text-danger text-left"
        : "text-success text-left";
    },
  },
};
</script>

<style scoped>
input[type="text"],
input[type="email"],
input[type="password"],
input[type="date"],
select,
textarea {
  display: block;
  width: 100%;
  padding: 0.4rem;
  font-size: 1.2rem;
  border: 1px solid #ccc;
}

.form-group {
  align-items: left;
}
.form-group label {
  text-align: left;
  float: left;
}

.form-group small {
  text-align: center;
  float: none;
}
</style>