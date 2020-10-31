import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import axios from "axios";

axios.interceptors.request.use((request) => {
  const headers = {
    "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
    "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
    "Content-Type": "application/json", // this shows the expected content type
  };
  request.headers = headers;

  console.log(request);
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
        console.log("okay, in 401 case");
        alert("Unauthorized access, please login.");
        this.props.history.push("/");
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
