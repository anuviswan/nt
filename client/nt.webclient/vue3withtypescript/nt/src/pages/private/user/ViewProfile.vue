<template>
  <div class="container-fluid">
    <UserProfileCard :user="currentUser" :isMiniCard="false" />
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import UserProfileCard from "@/components/private/user/UserProfileCard.vue";
import { useUserStore } from "@/stores/userStore";
import { User } from "@/types/UserTypes";

const store = useUserStore();
const currentUser = ref<User>({
  userName: store.UserName,
  displayName: store.DisplayName,
  bio: store.Bio,
});
</script>

<style scoped></style>
