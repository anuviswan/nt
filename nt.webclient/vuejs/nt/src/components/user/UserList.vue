<template>
  <div class="grid-contaniner">
    <div v-for="(user, index) in userList" :key="index">
      <UserCard v-bind:user="user" />
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
