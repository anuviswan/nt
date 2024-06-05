<template>
    <div>
        <ul class="user-grid">
          <li v-for="(user, index) in searchResults" :key="index">
            <UserProfileCard :user="user" :isMiniCard='true' :isReadOnly='true'/>  
          </li>
        </ul>
        <div>{{ searchTerm }}</div>
    </div>
</template>
<script setup lang="ts">

import {defineProps, withDefaults,ref, watch} from 'vue'
import {User} from '@/types/UserTypes'
import UserProfileCard from "@/components/private/user/UserProfileCard.vue";
import { userApiService } from '@/apiService/UserApiService';

interface Props{
    searchTerm : string
}

const props = withDefaults(defineProps<Props>(),{
    searchTerm : ''
});


const searchResults = ref<User[]>([
    {
        userName:'jia.anu',
        bio:'Hello, I am Jia',
        displayName:'Jia Anu'
    }
]);

watch(()=> props.searchTerm, async (newValue,oldValue)=> {
    console.log('Searching for ' + newValue);

    const response = await userApiService.searchUsers(newValue);

    console.log(response);

    if(response.hasError){
        // TODO : Error Handling
    }
    else{

        searchResults.value = response.users;
    }

})
</script>
<style scoped>
.user-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  gap: 16px;
  padding: 16px;
  grid-auto-rows: min-content; /* Ensure rows take the space they need */
}

.user-card {
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 8px;
  overflow: hidden;
  text-align: center;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
}

</style>