<template>
  <div class="row">
    <div
      v-for="(user, index) in userList"
      :key="index"
      class="col-md-3 col-6 my-1"
    >
      <UserCard v-bind:user="user" class="card h-100" />
    </div>
  </div>
</template>

<script>
import UserCard from "./UserCard";
export default {
  name: "UserList",
  props: {
    userList: {
      required: true,
    },
  },
  components: {
    UserCard,
  },
};
</script>

<style scoped>
.grid-container {
  display: grid;
  grid-template-columns: repeat(5, 1fr);
  grid-gap: 1rem;
}
</style>
