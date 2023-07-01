<template>
  <div class="container-fluid">
    <div class="row no-gutter">
      <!-- The image half -->
      <div class="col-md-6 d-none d-md-flex bg-indigo">
        <div
          class="text-white d-md-flex align-items-center h-100 p-5 text-center justify-content-center"
        >
          <div class="logoarea pt-5 pb-5">
            <i class="fa fa-film fa-4x fa-fw" aria-hidden="true"></i>
            <h1 class="display-4 navbar-brand">November Talkies</h1>
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

<script setup>
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
  background: #4b1076;
}

.navbar-brand {
  font-family: "Pacifico";
  font-size: xx-large;
  margin: 0px;
}
</style>
