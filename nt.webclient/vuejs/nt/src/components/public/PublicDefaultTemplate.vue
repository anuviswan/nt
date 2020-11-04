<template>
  <div class="container-fluid">
    <div class="row no-gutter">
      <!-- The image half -->
      <div class="col-md-6 d-none d-md-flex bg-image">
        <div
          class="text-white d-md-flex align-items-center h-100 p-5 text-center justify-content-center"
        >
          <div class="logoarea pt-5 pb-5">
            World of Cinemas !!
          </div>
        </div>
      </div>

      <!-- The content half -->
      <div class="col-md-6 bg-light">
        <div class="login d-flex align-items-center py-5">
          <!-- Demo content-->
          <div class="container">
            <div class="row">
              <div class="col-lg-10 col-xl-7 mx-auto">
                <h3 class="display-4">November Talkies!</h3>
                <p class="text-muted mb-4">
                  Movies - Nothing more.
                </p>
                <slot></slot>
              </div>
            </div>
          </div>
          <!-- End -->
        </div>
      </div>
      <!-- End -->
    </div>
  </div>
</template>

<script>
export default {
  name: "PublicDefaultTemplate",
};
</script>

<style scoped>
.login,
.image {
  min-height: 100vh;
}

.bg-image {
  background-image: url("https://res.cloudinary.com/mhmd/image/upload/v1555917661/art-colorful-contemporary-2047905_dxtao7.jpg");
  background-size: cover;
  background-position: center center;
}

.bg-indigo {
  background: indigo;
}
</style>
