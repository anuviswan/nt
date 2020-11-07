<template>
  <div class="container-fluid">
    <div class="row align-items-center justify-content-center">
      <div class="col-sm-12 col-md-10 col-lg-8">
        <div class="card">
          <div class="card-header font-weight-bold text-uppercase">
            Create Movie
          </div>
          <div class="card-body">
            <form
              class="form-horizontal needs-validation"
              v-on:submit="onSubmit"
            >
              <div class="form-group">
                <label for="movieTitle">Movie Title</label>
                <input
                  type="text"
                  placeholder="Movie Title"
                  name="movieTitle"
                  v-bind:class="
                    hasError('movieTitle')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <label for="language">language</label>
                <input
                  type="text"
                  v-model="language"
                  name="language"
                  placeholder="Language"
                  v-bind:class="
                    hasError('language')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <label for="releaseDate">releaseDate</label>
                <input
                  v-model="releaseDate"
                  placeholder="Date of Release"
                  name="releaseDate"
                  v-bind:class="
                    hasError('releaseDate')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <input type="submit" value="Save Changes" class="btn-primary" />
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
export default {
  name: "CreateMovie",
  data() {
    return {
      title: "",
      releaseDate: "",
      language: "",
      tags: "",
      errors: [],
      serverMessage: [],
      hasServerError: false,
    };
  },
  methods: {
    onSubmit() {},
    hasError(key) {
      return this.errors.indexOf(key) != -1;
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
