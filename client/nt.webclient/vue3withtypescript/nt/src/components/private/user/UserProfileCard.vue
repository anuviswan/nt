<template>
  <div class="container mt-5 d-flex justify-content-center">

    <!-- Mini Card Starts here -->
    <div class="card p-3 shadow"  v-if="isMiniCard">
      <div class="d-flex align-items-center">
        <div class="image">
          <img src="@/assets/user.png" class="rounded" width="155">
        </div>
        <div class="ml-3 w-100">
          <h4 class="mb-0 mt-0">{{ user.displayName }}</h4>
          <span>{{ user.userName }}</span>
          <p class="bio">{{ user.bio }}</p>

          <!-- Stats -->
          <div class="p-2 mt-2 bg-secondary d-flex justify-content-between rounded text-white stats">
            <div class="d-flex flex-column">
              <span class="stat-heading">Followers<i class="bi bi-people"></i></span>
              <span class="number1">{{ getValueOrDefault(user.countOfFollowers) }}</span>
            </div>

            <div class="d-flex flex-column">
              <span class="stat-heading">Following</span>
              <span class="number2">{{ getValueOrDefault(user.countOfFollowedUsers) }}</span>
            </div>

            <div class="d-flex flex-column">
              <span class="stat-heading">Rating</span>
              <span class="number3">{{ getValueOrDefault(user.Downrated) }}</span>
            </div>

          </div>

          <!--End User Stats-->

          <!-- Buttons -->

          <div class="button mt-2 d-flex flex-row align-items-center">
            <button class="btn btn-sm btn-primary w-100 ml-2" :disabled="!canFollow()">{{ followText() }}</button>
          </div>

          <!--End Buttons-->
        </div>
      </div>
    </div>
    <!-- Mini Card Ends here -->

    <!-- Details Card Starts here -->
    <div v-else>
    <div class="card p-3 shadow" >
      <div class="d-flex align-items-center">
        <div class="image">
          <img src="@/assets/user.png" class="rounded" width="155">
        </div>
        <div class="ml-3 w-100">
          <h4 class="mb-0 mt-0">{{ user.displayName }}</h4>
          <span>{{ user.userName }}</span>
          <p class="bio">{{ user.bio }}</p>

          <!-- Stats -->
          <div class="p-2 mt-2 bg-secondary d-flex justify-content-between rounded text-white stats">
            <div class="d-flex flex-column">
              <span class="stat-heading">Followers<i class="bi bi-people"></i></span>
              <span class="number1">{{ getValueOrDefault(user.countOfFollowers) }}</span>
            </div>

            <div class="d-flex flex-column">
              <span class="stat-heading">Following</span>
              <span class="number2">{{ getValueOrDefault(user.countOfFollowedUsers) }}</span>
            </div>

            <div class="d-flex flex-column">
              <span class="stat-heading">Rating</span>
              <span class="number3">{{ getValueOrDefault(user.Downrated) }}</span>
            </div>

          </div>

          <!--End User Stats-->

          <!-- Buttons -->

          <div class="button mt-2 d-flex flex-row align-items-center">
            <button class="btn btn-sm btn-primary w-100 ml-2" :disabled="!canFollow()">{{ followText() }}</button>
          </div>

          <!--End Buttons-->
        </div>
      </div>
    </div>
    <div class="card p-3 shadow" >
      
    </div>
  </div>
    <!-- Details Card Starts here -->
  </div>
</template>
<script setup lang="ts">
import {ref} from 'vue';
import { withDefaults, defineProps } from "vue"
import { User } from "@/types/UserTypes";
import {useUserStore } from '@/stores/userStore';

interface Props {
  user: User,
  isMiniCard: boolean
}

const store = useUserStore();
const currentUserName = ref(store.UserName);

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

const getValueOrDefault = (val: number | undefined) => val === null || val === undefined ? 0 : val;

function canFollow():boolean{
  console.log('Props:'+ props.user.userName)
  console.log('Store:'+ currentUserName.value)
  return props.user.userName != currentUserName.value;
}

function followText():string{

  console.log(canFollow());
  return canFollow()?"Follow": "Following";
}
</script>
<style scoped>
.card {
  width: 400px;
  border: 10;
  border-radius: 10px;
  background-color: #fff;
}



.stats {

  background: #f2f5f8 !important;

  color: #000 !important;
}

.bio {
  font-size: 12px;
  font-style: italic;
}

.stat-heading {
  font-size: 10px;
  color: #6a6b6e;
}

.number1 {
  font-weight: 500;
}

.number2 {
  font-weight: 500;
}

.number3 {
  font-weight: 500;
}
</style>