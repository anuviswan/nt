import React, { useReducer } from "react";
import UserContext from "./userContext";
import UserReducer from "./userReducer";
import { SET_CURRENT_USER } from "../types";

const UserState = (props) => {
  const initialState = {
    currentUser: {},
    userToken: "",
    isUserAuthenticated: false,
  };

  const [state, dispatch] = useReducer(UserReducer, initialState);

  // search user

  // setCurrentUser
  const setCurrentUser = async (userProfile) => {
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
        isUserAuthenticated: state.isUserAuthenticated,
        setCurrentUser,
      }}
    >
      {props.children}
    </UserContext.Provider>
  );
};

export default UserState;
