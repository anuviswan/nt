<template>
  <div class="row">
    <div class="col-lg-9">
      More details will come here - UNDER CONSTRUCTION
    </div>
    <div class="col-lg-3">
      <div class="card card-block rounded shadow shadow-sm">
        <div class="card-header bg-primary text-light text-uppercase">
          <div class="card-title  align-middle">
            <h5 class="mb-0">Sign Up</h5>
          </div>
        </div>
        <div class="card-body">
          <form class="form  needs-validation">
            <div class="form-group">
              <input
                type="text"
                class="form-control block"
                placeholder="Username"
              />
            </div>
            <div class="form-group">
              <input
                type="password"
                class="form-control block"
                placeholder="Password"
              />
            </div>

            <div class="form-group">
              <input
                type="password"
                class="form-control block"
                placeholder="Confirm Password"
              />
            </div>

            <div class="form-group">
              <input
                type="submit"
                class="btn btn-block btn-primary"
                value="Submit"
              />
            </div>
          </form>
          <div>
            Not a member ?
            <router-link to="/register">Sign up here</router-link>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Register',
};
</script>

<style></style>
