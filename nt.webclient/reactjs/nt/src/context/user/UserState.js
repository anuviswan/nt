import React, { useReducer } from "react";
import axios from "axios";
import UserContext from "./UserContext";
import UserReducer from "./UserReducer";
import { SET_CURRENT_USER } from "../types";

const UserState = (props) => {
  const initialState = {
    currentUser: {},
    userToken: "",
    isUserAuthenticated: false,
  };

  const [state, dispatch] = useReducer(UserReducer, initialState);

  // search user

  // validate user
  const validateUser = async (userProfile) => {
    dispatch({
      type: SET_CURRENT_USER,
      payload: userProfile,
    });
  };

  // register user

  return (
    <UserContext.Provider
      value={{
        currentUser: state.currentUser,
        userToken: state.userToken,
        validateUser,
      }}
    >
      {props.children}
    </UserContext.Provider>
  );
};

export default UserState;
