<template>
    <input id="profile-image-upload" ref="fileUploader" class="d-none" 
    type="file" @change="handleImageChanged">
    <div class="card">
        <img :src="imgSrc" 
        class="avataar image-center  rounded-circle card-img img-thumbnail mx-auto d-block" 
        alt="Profile Image">
        <div class="card-img-overlay d-flex" v-if="!props.isReadOnly">
            <div class="align-self-center mx-auto">
                <i class="fas fa-file-upload fa-2x fileupload" @click="browseImage()"></i>
            </div>
        </div>
        <input type="submit" value="Update" :on-click="uploadImageToServer()" v-if="isDirty">
    </div>
</template>
<script setup lang="ts">
import {onMounted, ref} from 'vue'
import { userApiService } from "@/apiService/UserApiService";
import { IUploadProfileImageRequest } from '@/types/apirequestresponsetypes/User';
import { withDefaults, defineProps } from "vue"

interface Props{
    userName: string,
    isReadOnly : boolean
}

const props = withDefaults(defineProps<Props>(),{
    isReadOnly : false
})
 

const currentUserName = ref(props.userName);

const fileUploader = ref<HTMLInputElement|null>(null);

// eslint-disable-next-line @typescript-eslint/no-var-requires    
const defaultImage = require('@/assets/DefaultProfile.jpg')
const imgSrc = ref<string|File>(defaultImage);
const isDirty = ref<boolean>(false);
const file = ref<File|null|undefined>(null);
const canExecute = ref<boolean>(false);
onMounted(()=>{
    getProfileImage();
    // check if profile image exist in blob
    //imgSrc.value = defaultImage;
})

const getProfileImage = async() :  Promise<void> => {
    var response  = await userApiService.getProfileImage({
        userName : props.userName
    });

    if (!isValidBlob(response)) {
        imgSrc.value = defaultImage;
        return;
      }
    console.log('imageFIle' + response);
    //const imageFile = await response;
    var path =  URL.createObjectURL(response)
    imgSrc.value = path;
}

const isValidBlob = (blob: Blob): boolean =>{
      return blob instanceof Blob && blob.size > 0;
    }
const browseImage = ():void=>{
    if(fileUploader?.value == null) return;
    fileUploader?.value?.click();
    return;
}

const handleImageChanged = (e:Event) :void => {
    file.value = (e.target as HTMLInputElement)?.files?.[0];
    if(file.value){

        console.log(file.value);
        const reader = new FileReader();
        reader.onload = (e)=>{
            canExecute.value = true;
            imgSrc.value = e.target?.result as string;
        }
        reader.readAsDataURL(file.value);
        isDirty.value = true;
    }
}

const uploadImageToServer = async ():Promise<void>=>{
    if(canExecute.value == false) return;
    console.log("Checking if ready to upload file")
    if(file.value && isDirty){
        console.log("Ready to upload file to server");

        
        const fileInfo:IUploadProfileImageRequest = {
                imageKey : currentUserName.value,
                file: file.value
        } ;

        console.log("sending request");
        canExecute.value = false;
        var response = await userApiService.uploadProfileImage(fileInfo);

        console.log(response);
    }
}

</script>
<style scoped>
    .avataar{
        vertical-align: middle;
        border-radius: 50%;
        max-width: 100%;
        height: auto;
    }

    .fileupload{
        opacity: 0.10
    }

    .fileupload:hover{
        opacity: 0.50;
    }
</style>