<template>
  <input
    id="profile-image-upload"
    ref="fileUploader"
    class="d-none"
    type="file"
    @change="handleImageChanged"
  />
  <div class="card">
    <img
      :src="imgSrc"
      class="avataar image-center rounded-circle card-img img-thumbnail mx-auto d-block"
      alt="Profile Image"
    />
    <span v-if="props.showDisplayName">{{ userProfile.displayName }}</span>
    <span v-if="props.showInitials">{{
      getInitials(userProfile.displayName)
    }}</span>
    <div class="card-img-overlay d-flex" v-if="!props.isReadOnly">
      <div class="align-self-center mx-auto">
        <i
          class="fas fa-file-upload fa-2x fileupload"
          @click="browseImage()"
        ></i>
      </div>
    </div>
    <input
      type="submit"
      value="Update"
      :on-click="uploadImageToServer()"
      v-if="isDirty"
    />
  </div>
</template>
<script setup lang="ts">
  import { onMounted, ref } from 'vue';
  import { userApiService } from '@/apiService/UserApiService';
  import { IUploadProfileImageRequest } from '@/types/apirequestresponsetypes/User';
  import { withDefaults, defineProps } from 'vue';

  interface Props {
    userName: string;
    isReadOnly: boolean;
    showUserName: boolean;
    showDisplayName: boolean;
    showInitials: false;
  }

  interface UserProfile {
    userName: string;
    displayName: string;
  }

  const props = withDefaults(defineProps<Props>(), {
    isReadOnly: false,
    showUserName: false,
    showDisplayName: false,
    showInitials: false,
  });

  const currentUserName = ref(props.userName);
  const userProfile = ref<UserProfile>({
    userName: props.userName,
    displayName: '',
  });

  const fileUploader = ref<HTMLInputElement | null>(null);

  // eslint-disable-next-line @typescript-eslint/no-var-requires
  // const defaultImage = require('@/assets/DefaultProfile.jpg')
  import defaultImage from '@/assets/DefaultProfile.jpg';
  const imgSrc = ref<string | File>(defaultImage);
  const isDirty = ref<boolean>(false);
  const file = ref<File | null | undefined>(null);
  const canExecute = ref<boolean>(false);
  onMounted(() => {
    getProfileImage();
    getUserProfile();
    // check if profile image exist in blob
    //imgSrc.value = defaultImage;
  });

  const getProfileImage = async (): Promise<void> => {
    var response = await userApiService.getProfileImage({
      userName: props.userName,
    });

    if (!isValidBlob(response)) {
      imgSrc.value = defaultImage;
      return;
    } else {
      console.log('imageFIle' + response);
      //const imageFile = await response;
      var path = URL.createObjectURL(response!);
      imgSrc.value = path;
    }
  };

  const getUserProfile = async (): Promise<void> => {
    var response = await userApiService.getUserProfile(props.userName);

    if (response.hasError) {
      // TODO : Error Handling
      return;
    }

    currentUserName.value = response.user.userName;
    userProfile.value = {
      userName: response.user.userName,
      displayName: response.user.displayName ?? '',
    };
  };

  const isValidBlob = (blob: Blob | null): boolean => {
    return blob instanceof Blob && blob.size > 0;
  };
  const browseImage = (): void => {
    if (fileUploader?.value == null) return;
    fileUploader?.value?.click();
    return;
  };

  const handleImageChanged = (e: Event): void => {
    file.value = (e.target as HTMLInputElement)?.files?.[0];
    if (file.value) {
      console.log(file.value);
      const reader = new FileReader();
      reader.onload = (e) => {
        canExecute.value = true;
        imgSrc.value = e.target?.result as string;
      };
      reader.readAsDataURL(file.value);
      isDirty.value = true;
    }
  };

  const uploadImageToServer = async (): Promise<void> => {
    if (canExecute.value == false) return;
    console.log('Checking if ready to upload file');
    if (file.value && isDirty) {
      console.log('Ready to upload file to server');

      const fileInfo: IUploadProfileImageRequest = {
        imageKey: currentUserName.value,
        file: file.value,
      };

      console.log('sending request');
      canExecute.value = false;
      var response = await userApiService.uploadProfileImage(fileInfo);

      console.log(response);
    }
  };

  const getInitials = (name: string): string | null => {
    if (name && name.length > 0) {
      return name
        .split(' ') // split into ["John", "Doe"]
        .map((part) => part[0]) // take first letter of each
        .join('') // join them back together
        .toUpperCase(); // make sure it's uppercase
    } else {
      return name;
    }
  };
</script>
<style scoped>
  .avataar {
    vertical-align: middle;
    border-radius: 50%;
    max-width: 100%;
    height: auto;
  }

  .fileupload {
    opacity: 0.1;
  }

  .fileupload:hover {
    opacity: 0.5;
  }
</style>
