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
export default {};
</script>

<style></style>
