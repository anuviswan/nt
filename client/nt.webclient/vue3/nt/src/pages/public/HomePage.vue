<template>
  <PublicDefaultTemplate>
    <LoginUser />
  </PublicDefaultTemplate>
</template>

<script>
import LoginUser from "@/components/public/LoginUser.vue";
import PublicDefaultTemplate from "@/components/public/PublicDefaultTemplate";
export default {
  name: "HomePage",
  components: {
    LoginUser,
    PublicDefaultTemplate,
  },
};
</script>

<style></style>
