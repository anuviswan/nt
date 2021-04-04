<template>
  <div class="card border-primary mb-3">
    <div class="card-avataar py-2">
      <img
        class=" rounded-circle img-thumbnail"
        src="https://cdn3.iconfinder.com/data/icons/avatars-flat/33/woman_9-512.png"
        alt="Avataar Pic"
        width="100"
      />
    </div>

    <div class="card-body mx-auto">
      <h4 class="card-title block text-uppercase text-center">
        {{ currentUser.displayName }}
      </h4>
      {{ currentUser.userName }}
      <!-- <Rating value="{5}" totalStars="{5}" /> -->
    </div>

    <div class="card-header">
      <div class="row">
        <div class="col-lg-4 card-metadata text-center">45</div>
        <div class="col-lg-4 card-metadata text-center">345</div>
        <div class="col-lg-4 card-metadata text-center">1.1K</div>
      </div>
      <div class="row">
        <div class="col-lg-4 card-metadata-footer text-center">Reviews</div>
        <div class="col-lg-4 card-metadata-footer text-center">Followed</div>
        <div class="col-lg-4 card-metadata-footer text-center">Followers</div>
      </div>
    </div>
    <div class="card-body mx-auto">{{ currentUser.bio }}</div>
  </div>
</template>
<script>
import { toRefs, reactive } from "vue";
export default {
  name: "UserCard",
  props: {
    user: {
      required: true,
      type: Object,
    },
  },
  setup(props) {
    console.log(props.user);
    const { user } = toRefs(props);
    console.log("Loading User Card");
    console.log(user.value);
    const currentUser = reactive(user);
    // let selectedUser = reactive({});
    // watch(() => {
    //   const { selectedUser } = props.user;
    // });
    // console.log("Loading User Card : " + props.user.userName);
    // console.log(props.user);
    // userName.value = props.user.userName;
    // displayName = props.user.displayName;
    // bio = props.user.bio;
    // rating = props.user.rating;

    // return () => {
    //   const { user } = props.user;
    //   console.log("Loading User Card");
    //   console.log(props.user);
    //   return user;
    // };

    return {
      currentUser,
    };
  },
};
</script>

<style scoped>
.card {
  max-width: 350px;
}
</style>
