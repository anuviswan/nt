import Vue from 'vue'
import App from './App.vue'
import router from "./router";
import axios from "axios";
import Loading from "vue-loading-overlay";
import "vue-loading-overlay/dist/vue-loading.css";
import VueSimpleAlert from "vue-simple-alert";

Vue.config.productionTip = false

Vue.use(Loading);
Vue.use(VueSimpleAlert);

let loader:any;

Vue.config.productionTip = false;
axios.defaults.baseURL = process.env.VUE_APP_BASE_URL;
axios.interceptors.request.use((request) => {
  showLoader();
  const header = getHttpHeader();
  request.headers = header;
  return request;
});

axios.interceptors.response.use(
  (response) => {
    hideLoader();

    return {
      data: response.data,
      hasError: false,
      error: [],
    };
  },
  (error) => {
    hideLoader();
    switch (error.response.status) {
      case 400:
        return {
          data: null,
          hasError: true,
          error: [error.response.data],
        };
      case 401:
        alert("Unauthorized access, please login.");
        router.push("/");
        break;
      case 500:
        // Handle 500 here
        break;
      // and so on..
    }
    // return Promise.reject(error);
    return {
      data: null,
      hasError: true,
      error: [error.response.data],
    };
  }
);

function showLoader() {
  if (loader) return;

  loader = Vue.$loading.show({ color: "#75B7EC", loader: "bars" });
}

function hideLoader() {
  loader && loader.hide();
  loader = null;
}

new Vue({
  router,
  render: h => h(App),
}).$mount('#app')
