<template>
    <div class="container-fluid">
      <div class="row align-items-center justify-content-center">

        <!-- Mini Card -->
        <div class="col-sm-12 col-md-6 col-lg-4" v-if="isMiniCard" width="400">
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
  import {User} from "@/types/UserTypes";
interface Props{
      user: User,
      isMiniCard : boolean
  }
    const props = withDefaults(defineProps<Props>(),{
      user : ()=>{
        return{
          userName :'DefaultUser',
          displayName : 'Default User',
          bio: 'Hello, I am Default User',
        }
      },
      isMiniCard : true
    });
  
</script>
<style scoped></style>