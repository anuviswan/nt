<template>
  <div>sasa</div>
</template>

<script lang="ts">
import { Component, Vue, Prop } from "vue-property-decorator";

@Component
export default class ValidationMessage extends Vue {
  @Prop() private messages!: Array<string>;
  @Prop() private isError!: boolean;

  constructor() {
    super();
  }
}
</script>
