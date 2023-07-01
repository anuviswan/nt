<template>
  <div>
    <NavigationBar />
    <div class="pt-5">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
import NavigationBar from "@/components/private/generic/NavigationBar";
export default {
  name: "PrivateContainer",
  components: { NavigationBar },
};
</script>

<style></style>
