import { SET_CURRENT_USER } from "../types";

export default (state, action) => {
  switch (action.type) {
    case SET_CURRENT_USER:
      return {
        ...state,
        currentUser: action.payload.currentUser,
        isUserAuthenticated: action.payload.isAuthenticated,
        userToken: action.payload.authToken,
      };
    default:
      return state;
  }
};
