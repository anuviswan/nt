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
                  v-model="title"
                  v-bind:class="
                    hasError('title')
                      ? 'form-control block is-invalid'
                      : 'form-control block'
                  "
                />
              </div>
              <div class="form-group">
                <label for="language">Language</label>
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
                <label for="releaseDate">Release Date</label>
                <v-date-picker v-model="releaseDate">
                  <template v-slot="{ inputValue, inputEvents }">
                    <input
                      v-bind:class="
                        hasError('tags')
                          ? 'form-control block is-invalid'
                          : 'form-control block'
                      "
                      :value="inputValue"
                      v-on="inputEvents"
                    />
                  </template>
                </v-date-picker>
              </div>
              <div class="form-group">
                <label for="tags">Cast And Crew</label>
                <input
                  v-model="tags"
                  placeholder="Cast And Crew"
                  name="tags"
                  v-bind:class="
                    hasError('tags')
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
import { createMovie } from "../../../api/movies";

export default {
  name: "CreateMovie",
  data() {
    return {
      title: "",
      releaseDate: new Date(),
      language: "",
      tags: "",
      errors: [],
      serverMessage: [],
      hasServerError: false,
    };
  },
  methods: {
    async onSubmit(e) {
      e.preventDefault();
      if (!this.validateForm()) {
        console.log(this.errors);
        console.log("Form is INVALID");
        return;
      }

      var movie = {
        title: this.title,
        language: this.language,
        releaseDate: this.releaseDate,
        actors: this.tags.split(","),
      };
      var response = await createMovie(movie);
      if (response.hasError) {
        this.hasServerError = true;
        this.serverMessage = response.error;
        return;
      }
      this.serverMessage.push("Movie created successfully");
    },
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
    validateForm() {
      let isValidFlag = true;
      this.errors = [];

      if (!this.title) {
        this.errors.push("title");
        isValidFlag = false;
      }

      if (!this.releaseDate) {
        this.errors.push("releaseDate");
        isValidFlag = false;
      }

      if (!this.language) {
        this.errors.push("language");
        isValidFlag = false;
      }

      return isValidFlag;
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
