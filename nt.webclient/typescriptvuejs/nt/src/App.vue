<template>
  <div id="app" class="container-fluid">
    <Shell />
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import Shell from "../src/pages/Shell.vue";

@Component({
  components: {
    Shell,
  },
})
export default class App extends Vue {}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
