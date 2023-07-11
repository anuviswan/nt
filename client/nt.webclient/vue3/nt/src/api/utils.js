import {useUserStore} from "@/store/user.js"

const getHttpHeader = () => {
  
  const headers = {
    "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
    "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
    "Content-Type": "application/json", // this shows the expected content type
  };

  const userStore = useUserStore();

  if(userStore.UserName){
    const authToken = userStore.Token;
    if (authToken) {
      headers.Authorization = `Bearer ${authToken}`;
    }
  }

  console.log(headers);
  return headers;
};

export { getHttpHeader };
