<template>
    <input id="profile-image-upload" ref="fileUploader" class="d-none" type="file" @change="handleImageChanged">
    <div class="card">
        <img :src="imgSrc" 
        class="avataar image-center  rounded-circle card-img img-thumbnail mx-auto d-block" 
        alt="Profile Image">
        <div class="card-img-overlay d-flex">
            <div class="align-self-center mx-auto">
                <i class="fas fa-file-upload fa-2x fileupload" @click="browseImage()"></i>
            </div>
        </div>
        <input type="button" value="Update" v-if="isDirty">
    </div>
</template>
<script setup lang="ts">
import {onMounted, ref} from 'vue'
const fileUploader = ref<HTMLInputElement|null>(null);

// eslint-disable-next-line @typescript-eslint/no-var-requires    
const defaultImage = require('@/assets/DefaultProfile.jpg')
const imgSrc = ref<string>(defaultImage);
const isDirty = ref<boolean>(false);

onMounted(()=>{
    imgSrc.value = defaultImage;
})

const browseImage = ():void=>{
    if(fileUploader?.value == null) return;
    fileUploader!.value!.click();
    return;
}

const handleImageChanged = async (e:Event) : Promise<void> => {
    const file = (e.target as HTMLInputElement)!.files?.[0];
    
    if(file){

        console.log(file);
        const reader = new FileReader();
        reader.onload = (e)=>{
            imgSrc.value = e.target?.result as string;
        }
        reader.readAsDataURL(file);
        isDirty.value = true;


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