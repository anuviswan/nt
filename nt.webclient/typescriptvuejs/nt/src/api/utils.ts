import store from "../store/index";
import {namespace} from "vuex-class"

const user = namespace('UserStore');

const getHttpHeader = () => {
  const authToken = store.getters.UserStore.currentUser.token;
  const headers = {
    "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
    "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
    "Content-Type": "application/json", // this shows the expected content type
  };

  if (authToken) {
    headers.Authorization = `Bearer ${authToken}`;
  }
  return headers;
};

export { getHttpHeader };
