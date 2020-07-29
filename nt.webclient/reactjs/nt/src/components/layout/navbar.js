import React, { useContext } from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import "../../App.css";
import UserContext from "../../context/user/userContext";

const Navbar = ({ icon, title }) => {
  const userContext = useContext(UserContext);
  console.log("here");
  console.log(userContext.currentUser);
  return (
    <nav className='navbar bg-primary py-2'>
      <h4>
        <i className={icon} /> {title}
      </h4>
      <ul>
        <li>
          <Link to='/'>Home</Link>
        </li>
        <li>
          <Link
            to={{ pathname: "/user", state: { user: userContext.currentUser } }}
          >
            {userContext.currentUser.userName}
          </Link>
        </li>
        <li>
          <Link to='/searchUser'>
            <i className='fa fa-search' aria-hidden='true'></i>
          </Link>
        </li>
      </ul>
    </nav>
  );
};

Navbar.propTypes = {
  title: PropTypes.string.isRequired,
  icon: PropTypes.string.isRequired,
};
export default Navbar;
