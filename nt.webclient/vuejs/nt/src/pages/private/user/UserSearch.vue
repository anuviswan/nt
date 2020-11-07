<template>
  <div>
    <div>
      <SearchBar v-on:searched="onSearch" promptString="Search Users..." />
    </div>
    <div v-if="userList.length">
      <UserList v-bind:userList="userList" />
    </div>
  </div>
</template>

<script>
import SearchBar from "../../../components/generic/SearchBar";
import UserList from "../../../components/user/UserList";
import { searchUser } from "../../../api/user";
export default {
  name: "UserSearch",
  components: { SearchBar, UserList },
  data() {
    return {
      userList: [],
    };
  },
  methods: {
    async onSearch(key) {
      const response = await searchUser(key);
      console.log(response);
      if (response.hasError) {
        console.log(response.error);
      } else {
        console.log(response.data);
        this.userList = response.data;
      }
    },
  },
};
</script>

<style></style>
