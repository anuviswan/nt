<template>
  <div>
    <div>
      <SearchBar v-on:searched="onSearch" />
    </div>
    <div v-if="userList.length">
      <UserList v-bind:userList="userList" />
    </div>
  </div>
</template>

<script>
import SearchBar from "../../components/generic/SearchBar";
import UserList from "../../components/user/UserList";
import { searchUser } from "../../api/user";
export default {
  name: "Dashboard",
  components: { SearchBar, UserList },
  data() {
    return {
      userList: [],
    };
  },
  methods: {
    async onSearch(key, searchType) {
      switch (searchType) {
        case "USER": {
          const response = await searchUser(key);
          console.log(response);
          if (response.hasError) {
            console.log(response.error);
          } else {
            console.log(response.data);
            this.userList = response.data;
          }
          break;
        }
      }
    },
  },
};
</script>

<style></style>
