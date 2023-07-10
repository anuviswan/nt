import { computed,ref } from "vue";
import {defineStore} from "pinia"


export const useUserStore = defineStore('UserStore',()=>{
    // state
    const userName = ref('');
    const displayName = ref('');
    const bio = ref('');
    const token = ref('');
    const rating = ref('');
    // getters

    const UserName = computed(()=> userName.value);
    const DisplayName = computed(()=> displayName.value ? displayName.value : userName.value);
    const Bio = computed(()=> bio.value);
    const Token = computed(()=> token.value);
    const Rating = computed(()=> rating.value);

    // methods

    const SaveUser = (user) => {
        userName.value = user.userName;
        displayName.value = user.displayName;
        bio.value = user.bio;
        token.value = user.token;
    };

    const Reset = () => {
        userName.value = '';
        displayName.value = '';
        bio.value = '';
        token.value = '';
    }
    return{
        UserName,
        DisplayName,
        Bio,
        Token,
        Rating,
        SaveUser,
        Reset
    }
});