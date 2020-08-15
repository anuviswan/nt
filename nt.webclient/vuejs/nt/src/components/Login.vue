<template>
  <div class="card rounded shadow shadow-sm">
    <div class="card-header bg-primary">
      <div class="card-title">
        <h3 class="mb-0">Sign In</h3>
      </div>
    </div>
    <div class="card-body">
      <form class="form needs-validation">
        <input type="text" name="userName" placeholder="UserName" />
        <input type="password" name="passKey" placeholder="Password" />
        <input type="submit" value="Login" class="bg-primary btn-block" />
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

<style>
</style>