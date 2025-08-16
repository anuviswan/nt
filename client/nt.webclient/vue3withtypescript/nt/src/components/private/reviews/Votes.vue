<script setup lang="ts">
  import { defineProps, withDefaults, computed } from 'vue';
  interface Props {
    IsUpvote: boolean;
    Count: number;
    Text: string;
    IsSelected: boolean;
  }

  const props = withDefaults(defineProps<Props>(), {
    IsUpvote: true,
    Text: 'Upvoted',
    Count: 32,
    IsSelected: false,
  });

  const iconClass = computed(() => {
    if (props.IsUpvote) {
      return props.IsSelected
        ? 'bi bi-hand-thumbs-up-fill'
        : 'bi bi-hand-thumbs-up';
    } else {
      return props.IsSelected
        ? 'bi bi-hand-thumbs-down-fill'
        : 'bi bi-hand-thumbs-down';
    }
  });
</script>
<template>
  <div class="d-flex align-items-center text-muted">
    <i :class="iconClass" class="me-2"></i>
    <span>{{ Count }} {{ Text }}</span>
  </div>
</template>
