<template>
    <input id="profile-image-upload" class="d-none" type="file">
    <div class="card">
        
        <img src="https://mdbootstrap.com/img/Photos/Others/placeholder-avatar.jpg"
        class="avataar image-center  rounded-circle card-img img-thumbnail">
        <div class="card-img-overlay align-self-center">
            <div class="card-text" >upload</div>
        </div>
    </div>
</template>
<script setup lang="ts"></script>
<style scoped>
    .avataar{
        vertical-align: middle;
  width: 300px;
  height: 300px;
  border-radius: 50%;
    }
</style>