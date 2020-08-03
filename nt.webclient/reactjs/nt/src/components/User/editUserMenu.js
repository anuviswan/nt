import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

const EditUserMenu = ({ user, selected }) => {
  return (
    <ul className='list-group'>
      <li
        className={`list-group-item  ${
          selected == EDIT_PROFILE ? "list-group-item-secondary" : ""
        }`}
      >
        {" "}
        <Link to={{ pathname: "/edituser", state: { user: user } }}>
          Edit User Profile
        </Link>
      </li>
      <li
        className={`list-group-item  ${
          selected == CHANGE_PWD ? "list-group-item-secondary" : ""
        }`}
      >
        <Link to={{ pathname: "/changepassword", state: { user: user } }}>
          Change Password
        </Link>
      </li>
    </ul>
  );
};

EditUserMenu.propTypes = {
  user: PropTypes.object.isRequired,
  selected: PropTypes.string.isRequired,
};

export default EditUserMenu;
export const EDIT_PROFILE = "Edit Profile";
export const CHANGE_PWD = "Change Password";
