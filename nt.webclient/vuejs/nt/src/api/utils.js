import store from "../store/index";

const getHttpHeader = () => {
  const authToken = store.getters.currentUser.token;
  console.log(authToken);
  const headers = {
    "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
    "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
    "Content-Type": "application/json", // this shows the expected content type
    Authorization: `Bearer ${authToken}`,
  };
  return headers;
};

export { getHttpHeader };
