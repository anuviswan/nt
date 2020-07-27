import React, { useContext } from "react";
import { Route, Redirect } from "react-router-dom";
import Navbar from "../layout/Navbar";
const PrivateRoute = ({ component: Component, ...rest }) => {
  return <Route {...rest} render={(props) => <Component {...props} />} />;
};

export default PrivateRoute;
