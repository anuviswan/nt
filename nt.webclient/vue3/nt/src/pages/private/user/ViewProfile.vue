<template>
  <div class="row">
    <div class="col-md-4 col-lg-3 col-lx-2">
      <UserCard v-bind:user="user" />
    </div>
    <div class="col-md-8 col-lg-9 col-lx-10">
      <div class="card">
        <div class="card-header">Movie Reviews</div>
        <div class="card-body">
          <h5 class="card-title">Dil Bechara</h5>
          <p class="card-text text-wrap">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec
            placerat neque. Praesent ipsum sem, posuere at tortor sed, pharetra
            vestibulum metus. Suspendisse id porttitor tortor. Praesent
            efficitur ante lectus, a suscipit mi mollis eget. Sed eu purus vel
            mauris laoreet tempus eu non turpis. Nam in porttitor erat. Mauris
            non lorem mi. Praesent ac hendrerit quam.
          </p>
        </div>
        <div class="card-body">
          <h5 class="card-title">Life of Pie</h5>
          <p class="card-text">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean nec
            placerat neque. Praesent ipsum sem, posuere at tortor sed, pharetra
            vestibulum metus. Suspendisse id porttitor tortor. Praesent
            efficitur ante lectus, a suscipit mi mollis eget. Sed eu purus vel
            mauris laoreet tempus eu non turpis. Nam in porttitor erat. Mauris
            non lorem mi. Praesent ac hendrerit quam.
          </p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from "vue";
import UserCard from "@/components/private/user/UserCard";

export default {
  name: "ViewProfile",
  components: {
    UserCard,
  },
  setup() {
    const user = ref({
      userName: "JiaNaina",
      displayName: "AamiNiks",
      rating: 5,
    });

    console.log("Default User");
    console.log(user.value);
    return { user };
  },
};
</script>

<style></style>
