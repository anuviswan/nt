<template>
  <div>
    <NavigationBar />
    <div class="pt-5 w-full">
      <router-view class="w-full"></router-view>
    </div>
  </div>
</template>

<script setup lang="ts">
  import NavigationBar from '@/components/private/generic/NavigationBar.vue';
</script>

<style></style>
