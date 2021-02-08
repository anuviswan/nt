<template>
  <DefaultTemplate>
    <Login />
  </DefaultTemplate>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
import DefaultTemplate from "../components/public/DefaultTemplate.vue";
import Login from "../components/public/Login.vue";

@Component({
  components: {
    DefaultTemplate,
    Login,
  },
})
export default class Shell extends Vue {}
</script>
<style></style>
