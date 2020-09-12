<template>
  <div>
    <Navbar />
    <h1>User has successfully logged in, This is Dashboard</h1>
  </div>
</template>

<script>
import Navbar from "../../components/Navbar";
export default {
  name: "PrivateContainer",
  components: {
    Navbar,
  },
};
</script>

<style>
</style>