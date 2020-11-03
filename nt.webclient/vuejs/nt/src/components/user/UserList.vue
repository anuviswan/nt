<template>
  <div>
    <div v-for="(user, index) in userList" :key="index">
      <UserCard v-bind:userName="user.userName" />
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

<style></style>
