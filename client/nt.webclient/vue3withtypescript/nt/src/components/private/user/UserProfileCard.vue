<template>
  <div class="container-fluid">
    <div class="row align-items-center justify-content-center">

      <!-- Mini Card -->
      <div class="col-sm-12 col-md-6 col-lg-2" v-if="isMiniCard" width="400">
        <div>
          <div class="card card-block rounded shadow shadow-sm">

            <div class="card-header bg-primary text-light ">
              <div class="align-middle">
                <h5 class="card-title">{{ user.displayName }} Mini</h5>
              </div>
            </div>
            <div class="align-middle">
              <img class="card-img-top " src="@/assets/user.png" alt="Card image cap">
            </div>
            <div class="card-body">
              <div class="align-middle">
                <h5 class="card-title">{{ user.userName }}</h5>
              </div>
              <div class="align-middle">
                <p class="card-text">{{ user.bio }}</p>
              </div>
              <div class="row">
                <div class="col-6 border-right">
                  <div class="pt-3 pb-3 pl-0 pr-0 text-center">
                    <h5 class="m-1">108k</h5>
                    <p class="m-0">Followers</p>
                  </div>
                </div>
                <div class="col-6">
                  <div class="pt-3 pb-3 pl-0 pr-0 text-center">
                    <h5 class="m-1">108k</h5>
                    <p class="m-0">Following</p>
                  </div>
                </div>
              </div>

            </div>
          </div>
        </div>
      </div>
      <!-- Full Card -->
      <div class="col-sm-12 col-md-6 col-lg-4" v-else>
        <div>
          <div class="card card-block rounded shadow shadow-sm">
            <div class="card-header bg-primary text-light ">
              <div class="card-title align-middle">
                <h5 class="mb-0">{{ user.displayName }} Mini</h5>
              </div>
            </div>
            <div class="card-body">
              <div class="text--primary align-middle">
                <b class="mb-0">{{ user.userName }}</b>
              </div>
              <div class="card-text align-middle">
                <i class="mb-0">{{ user.bio }}</i>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { withDefaults, defineProps } from "vue"
import { User } from "@/types/UserTypes";
interface Props {
  user: User,
  isMiniCard: boolean
}
const props = withDefaults(defineProps<Props>(), {
  user: () => {
    return {
      userName: 'DefaultUser',
      displayName: 'Default User',
      bio: 'Hello, I am Default User',
    }
  },
  isMiniCard: true
});

</script>
<style scoped>
.card-img-top {
  width: 50%;
  height: 10vh;
  object-fit: cover;
}
</style>