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

// Search User
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


// Register User / user sign up
const registerUser = async (userName, displayName, passKey) => {
  const userDetails = {
    userName: userName,
    passKey: passKey,
    displayName: displayName,
  };

  const response = await axios.post(
    "https://localhost:44353/api/User/CreateUser",
    userDetails
  );

  return response;
};

// Follow User
const followUser = async(userNameToFollow)=>{

  const request = {
    userToFollow : userNameToFollow
  };

  const response = await axios.post('https://localhost:44353/api/User/FollowUser',request);

  return response;
}

// Follow User
const unfollowUser = async(userNameToFollow)=>{

  console.log("unfollowing user")
  const request = {
    userToUnfollow : userNameToFollow
  };

  const response = await axios.post('https://localhost:44353/api/User/unfollowUser',request);
  return response;
  
};

export {
  changePassword,
  validateUser,
  getUser,
  updateUserProfile,
  searchUser,
  registerUser,
  followUser,
  unfollowUser
};
