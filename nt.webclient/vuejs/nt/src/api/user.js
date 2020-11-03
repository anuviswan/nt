import axios from "axios";
// Validate User
const validateUser = async (userName, passKey) => {
  const userDetails = {
    userName: userName,
    passKey: btoa(passKey),
  };

  const response = await axios.post(
    "https://localhost:44353/api/User/ValidateUser",
    userDetails
  );
  return response;
};

// Get User Profile
const getUser = async (userName) => {
  const params = {
    params: {
      userName: userName,
    },
  };

  var response = await axios.get(
    "https://localhost:44353/api/User/GetUser",
    params
  );
  return response;
};

// Update Profile
const updateUserProfile = async (user) => {
  const userDetails = {
    displayName: user.displayName,
    bio: user.bio,
  };

  var response = await axios.post(
    "https://localhost:44353/api/User/UpdateUser",
    userDetails
  );
  return response;
};

// Change Password
const changePassword = async (oldPassword, newPassword) => {
  var recordToUpdate = {
    oldPassword: oldPassword,
    newPassword: newPassword,
  };

  var response = await axios.post(
    "https://localhost:44353/api/User/ChangePassword",
    recordToUpdate
  );
  return response;
};

const searchUser = async (searchTerm) => {
  const params = {
    params: {
      partialString: searchTerm,
    },
  };
  var response = await axios.get(
    "https://localhost:44353/api/User/SearchUser",
    params
  );

  console.log(response);
  return response;
};

export { changePassword, validateUser, getUser, updateUserProfile, searchUser };
