<template>
    <div>
        <ul class="error-list">
          <li v-for="(user, index) in searchResults" :key="index">
            <UserProfileCard :user="user" :isMiniCard='false'/>
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
import { ISearchUsersRequest } from '@/types/apirequestresponsetypes/User';
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

watch(()=> props.searchTerm, async (oldValue,newValue)=> {
    console.log('Searching for ' + props.searchTerm);

    const searchRequest : ISearchUsersRequest = {
        searchTerm : newValue
    }; 
    const response = await userApiService.searchUsers(searchRequest);

    console.log(response);

})
</script>
<style scoped>
</style>