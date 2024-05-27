import {LoggedInUser} from "@/types/StoreTypes";
import { defineStore } from "pinia";
import { computed, ref } from "vue";

export const useUserStore = defineStore('UserStore',()=>{
 
    // state
    const loggedInUser = ref<LoggedInUser>({
        userName : '',
        displayName : '',
        bio : '',
        token : ''
    });

    // getters
    const UserName = computed(()=> loggedInUser.value.userName);
    const DisplayName = computed(() => loggedInUser.value.displayName ? loggedInUser.value.displayName : loggedInUser.value.userName);
    const Bio = computed(()=> loggedInUser.value.bio ? loggedInUser.value.bio  : '');
    const Token = computed(( )=> loggedInUser.value.token)

    // methods
    const SaveUser = (user:LoggedInUser):void=>{
        loggedInUser.value = user;
    }

    const Reset = ():void => {
        loggedInUser.value = {
            userName : '',
            displayName : '',
            bio : '',
            token : ''
        } as LoggedInUser;
    
    }

    return {
        loggedInUser,
        UserName,
        DisplayName,
        Bio,
        Token,
        SaveUser,
        Reset
    };
});