<template>
    <div class="container-fluid">
      <div class="row align-items-center justify-content-center">
        <div class="col-sm-8 col-md-6 col-lg-6">
          <div class="card card-block rounded shadow shadow-sm">
            <div class="card-header bg-primary text-light ">
            <div class="card-subtitle align-middle">
                <h4 class="mb-0">{{ user.displayName }}</h4>
            </div>
        </div>
            <div class="card-body">
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
      user: User
  }
    const props = withDefaults(defineProps<Props>(),{
      user : ()=>{
        return{
          userName :'DefaultUser',
          displayName : 'Default User',
          bio: 'Hello, I am Default User',
        }
      },
    });
  
</script>
<style scoped></style>