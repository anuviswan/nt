import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import axios from "axios";

axios.interceptors.request.use((request) => {
  const authToken = request.headers.Authorization;
  const headers = {
    "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
    "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
    "Content-Type": "application/json", // this shows the expected content type
    Accept: "application/json",
  };

  if (authToken) {
    headers.Authorization = authToken;
  }
  request.headers = headers;
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
    switch (error.response.status) {
      case 400:
        const errorMessages = error.response.data.errors;
        if (typeof error.response.data == "string") {
          return {
            data: null,
            hasError: true,
            error: [error.response.data],
          };
        }
        console.log(error.response.data);
        let errorCollection = [];
        for (var key in errorMessages) {
          // skip loop if the property is from prototype
          if (!errorMessages.hasOwnProperty(key)) continue;

          var obj = errorMessages[key];
          for (var prop in obj) {
            // skip loop if the property is from prototype
            if (!obj.hasOwnProperty(prop)) continue;

            // your code
            errorCollection.push(obj[prop]);
          }
        }

        return {
          data: null,
          hasError: true,
          error: errorCollection,
        };

      case 401:
        console.log("okay, in 401 case");
        alert("Unauthorized access, please login.");
        // window.location.href = "/";
        break;
      case 500:
        // Handle 500 here
        break;
      // and so on..
      default:
        break;
    }
    // return Promise.reject(error);
    return {
      data: null,
      hasError: true,
      error: [error.response.data],
    };
  }
);

ReactDOM.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
