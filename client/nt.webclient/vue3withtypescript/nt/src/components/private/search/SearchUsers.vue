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

import {defineProps, withDefaults,ref} from 'vue'
import {User} from '@/types/UserTypes'
import UserProfileCard from "@/components/private/user/UserProfileCard.vue";

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
</script>
<style scoped>
</style>