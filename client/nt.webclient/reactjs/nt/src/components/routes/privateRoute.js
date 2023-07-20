import React, { useContext } from "react";
import { Navigate } from "react-router-dom";
import UserContext from "../../context/user/userContext";

const PrivateRoute = ({children}) =>{
  const userContext = useContext(UserContext);

  if(!userContext.isUserAuthenticated){
    <Navigate to="/"/>
  }

  return children;
}
// const PrivateRoute1 = ({ component: Component, ...rest }) => {
//   const userContext = useContext(UserContext);
//   return (
//     <Route
//       {...rest}
//       render={(props) => {
//         if (userContext.isUserAuthenticated) {
//           return <Component {...props} />;
//         } else {
//           return (
//             <Navigate to={{ pathname: "/", state: { from: props.location } }} />
//           );
//         }
//       }}
//     />
//   );
// };

export default PrivateRoute;
