<template>
    <input id="profile-image-upload" class="d-none" type="file">
    <div class="card">
        
        <img src="https://mdbootstrap.com/img/Photos/Others/placeholder-avatar.jpg"
        class="avataar image-center  rounded-circle card-img img-thumbnail mx-auto d-block" alt="Profile Image">
        <div class="card-img-overlay d-flex">
            <div class="align-self-center mx-auto"><i class="fas fa-file-upload fa-2x fileupload"></i></div>
        </div>
    </div>
</template>
<script setup lang="ts"></script>
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