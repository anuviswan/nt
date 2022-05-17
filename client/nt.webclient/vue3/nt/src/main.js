import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import axios from "axios";
import { getHttpHeader } from "./api/utils";
//import VueSimpleAlert from "vue-simple-alert";



//Vue.use(VCalendar);

// Init plugin
// Vue.use(Loading);
// Vue.use(VueSimpleAlert);

createApp(App).use(store)
              .use(router)
            //  .use(VueSimpleAlert)
            //  .use(VCalendar)
              .mount('#app')



//Vue.config.productionTip = false;
axios.defaults.baseURL = process.env.VUE_APP_BASE_URL;
axios.interceptors.request.use((request) => {
  const header = getHttpHeader();
  request.headers = header;
  return request;
});


axios.interceptors.response.use(
    (response) => {
  
      return {
        data: response.data,
        hasError: false,
        error: [],
      };
    },
    (error) => {
      console.log(error);
      switch (error.response.status) {
        case 400:
          return {
            data: null,
            hasError: true,
            error: [error.response.data],
          };
        case 401:
          router.push("/");
          return {
            data:null,
            hasError:true,
            errorCode: 401,
            error:[error.response.data]
          }
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
  