<script setup lang="ts">
  import { defineProps } from 'vue';
  interface Props {
    IsUpvote: boolean;
    Count: number;
    Text: string;
  }

  const props = defineProps<Props>();
</script>
<template>
  <div></div>
</template>
