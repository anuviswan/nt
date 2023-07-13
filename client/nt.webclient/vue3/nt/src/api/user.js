import axios from "axios";
// Validate User
const validateUser = async (userName, passKey) => {
  const userDetails = {
    userName: userName,
    passKey: btoa(passKey),
  };

  const response = await axios.post(
    "api/user/validateuser",
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

  const response = await axios.post(
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

  return response;
};

const registerUser = async (userName, displayName, passKey) => {
  const userDetails = {
    userName: userName,
    passKey: btoa(passKey),
    displayName : displayName,
  };

  const response = await axios.post(
    "api/user/createuser",
    userDetails
  );

   console.log(response);
  return response;
};


const followUser = async(userNameToFollow)=>{

  const request = {
    userToFollow : userNameToFollow
  };

  const response = await axios.post('https://localhost:44353/api/User/FollowUser',request);
  return response;
  
}

export {
  changePassword,
  validateUser,
  getUser,
  updateUserProfile,
  searchUser,
  registerUser,
  followUser
};
