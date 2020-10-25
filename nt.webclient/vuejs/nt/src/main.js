import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import axios from "axios";
import Loading from "vue-loading-overlay";
import "vue-loading-overlay/dist/vue-loading.css";
// Init plugin
Vue.use(Loading);

// Would store loader instance in between loading
let loader;

Vue.config.productionTip = false;
axios.defaults.baseURL = process.env.VUE_APP_BASE_URL;
axios.interceptors.request.use((request) => {
  loader = Vue.$loading.show({ color: "#75B7EC", loader: "bars" });
  return request;
});

axios.interceptors.response.use(
  (response) => {
    hideLoader();
    return response;
  },
  (error) => {
    hideLoader();
    return Promise.reject(error);
  }
);

function hideLoader() {
  loader && loader.hide();
  loader = null;
}
new Vue({
  router,
  store,
  render: (h) => h(App),
}).$mount("#app");
