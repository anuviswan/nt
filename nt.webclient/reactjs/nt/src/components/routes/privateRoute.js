import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";
import UserContext from "../../context/user/userContext";
import Navbar from "../layout/Navbar";

const PrivateRoute = ({ component: Component, ...rest }) => {
  const userContext = useContext(UserContext);

  return (
    <Route
      {...rest}
      render={(props) => {
        if (userContext.currentUser.isAuthenticated) {
          return (
            <Navbar title='November Talkies' icon='fa fa-film'>
              {" "}
              <Component {...props} />
            </Navbar>
          );
        } else {
          return (
            <Redirect to={{ pathname: "/", state: { from: props.location } }} />
          );
        }
      }}
    />
  );
};

export default PrivateRoute;
