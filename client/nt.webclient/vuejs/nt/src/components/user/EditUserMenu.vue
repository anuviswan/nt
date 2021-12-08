<template>
  <ul class="list-group">
    <li>
      <router-link to="./edit" class="list-group-item"
        >Edit Profile</router-link
      >
    </li>
    <li>
      <router-link
        to="./chpwd"
        class="list-group-item list-group-item-secondary"
        >Change Password</router-link
      >
    </li>
  </ul>
</template>

<script>
export default {
  name: "EditUserMenu",
};
</script>

<style scoped>
ui {
  list-style-type: none;
}

li {
  list-style: none;
}
</style>