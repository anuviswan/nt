import axios from "axios";
import store from "../store/index";
// Validate User
const validateUser = async (userName, passKey) => {
  const userDetails = {
    userName: userName,
    passKey: btoa(passKey),
  };

  try {
    const response = await axios.post(
      "https://localhost:44353/api/User/ValidateUser",
      userDetails
    );
    return {
      data: response.data,
      hasError: false,
      error: [],
    };
  } catch (error) {
    return {
      data: null,
      hasError: true,
      error: [error.response.data],
    };
  }
};

// Get User Profile
const getUser = async (userName) => {
  const params = {
    params: {
      userName: userName,
    },
  };

  try {
    var response = await axios.get(
      "https://localhost:44353/api/User/GetUser",
      params
    );
    return {
      data: response.data,
      hasError: false,
      error: [],
    };
  } catch (error) {
    if (error.response.status == 400) {
      return {
        data: null,
        hasError: true,
        error: [error.response.data],
      };
    }
  }
};

// Update Profile
const updateUserProfile = async (user) => {
  const authToken = store.getters.currentUser.token;

  const headers = {
    "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
    "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
    "Content-Type": "application/json", // this shows the expected content type
    Authorization: `Bearer ${authToken}`,
  };

  const userDetails = {
    displayName: user.displayName,
    bio: user.bio,
    token: authToken,
  };

  try {
    var response = await axios.post(
      "https://localhost:44353/api/User/UpdateUser",
      userDetails,
      { headers: headers }
    );
    console.log(response);

    return {
      data: response.data,
      hasError: false,
      error: [],
    };
  } catch (error) {
    if (error.response.status == 400) {
      return {
        data: null,
        hasError: true,
        error: [error.response.data],
      };
    }
  }
};

// Change Password
const changePassword = async (oldPassword, newPassword) => {
  console.log(oldPassword);
  console.log(newPassword);
};

export { changePassword, validateUser, getUser, updateUserProfile };
