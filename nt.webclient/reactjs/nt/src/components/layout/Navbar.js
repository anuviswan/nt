import React from "react";
import PropTypes from "prop-types";
import { Link } from "react-router-dom";
import "../../App.css";

const Navbar = ({ icon, title }) => {
  console.log(icon);
  return (
    <nav className='navbar bg-primary'>
      <h1>
        <i className={icon} /> {title}
      </h1>
      <ul>
        <li>
          <Link to='/'>Home</Link>
        </li>
        <li>
          <Link to='/profile'>Profile</Link>
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
