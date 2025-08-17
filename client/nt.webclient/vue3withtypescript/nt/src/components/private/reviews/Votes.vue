<script setup lang="ts">
  import { defineProps, withDefaults, computed } from 'vue';
  import Vote from './Vote.vue';
  interface Props {
    IsUpvote: boolean;
    Count: number;
    Text: string;
    IsSelected: boolean;
    Alignment: 'Right' | 'Left';
  }

  const props = withDefaults(defineProps<Props>(), {
    IsUpvote: true,
    Text: 'Upvoted',
    Count: 32,
    IsSelected: false,
    Alignment: 'Left',
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
  <div>
    <div
      v-if="Alignment == 'Left'"
      class="d-flex flex-column align-items-start text-muted"
    >
      <i :class="iconClass"></i>
      <span>{{ Count }} {{ Text }}</span>
    </div>
    <div
      v-if="Alignment == 'Right'"
      class="d-flex flex-column align-items-end text-muted"
    >
      <i :class="iconClass"></i>
      <span>{{ Count }} {{ Text }}</span>
    </div>
  </div>
</template>
