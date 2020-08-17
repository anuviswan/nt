<template>
  <div class="card rounded shadow shadow-sm">
    <div class="card-header bg-primary">
      <div class="card-title">
        <h3 class="mb-0">Sign In</h3>
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
      </form>
      <div>Sign up</div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
};
</script>

<style scoped></style>
