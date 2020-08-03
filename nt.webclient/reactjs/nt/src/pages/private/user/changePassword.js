import React, { useContext, useState } from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import Axios from "axios";
import UserContext from "../../../context/user/userContext";
import ValidationMessage from "../../../components/layout/validationMessage";
import EditUserMenu, {
  CHANGE_PWD,
} from "../../../components/User/editUserMenu";

const ChangePassword = ({ location }) => {
  const userContext = useContext(UserContext);
  const authToken = userContext.userToken;

  const [formValidation, setFormValidation] = useState({
    isVisible: false,
    isValid: true,
    message: "",
  });

  const { user } = location.state;
  const [userDetails, setUserDetails] = useState({
    oldPassword: "",
    newPassword: "",
    confirmPassword: "",
  });

  const onChange = (e) => {};
  const onSubmit = (e) => {};

  return (
    <div className='container-fluid '>
      <div className='row'>
        <div className='col-lg-2'>
          <EditUserMenu user={user} selected={CHANGE_PWD} />
        </div>
        <div className='col-lg-10'>
          <div className='card'>
            <div className='card-header'>Change Password</div>
            <div className='card-body'>
              <form className='form needs-validation' onSubmit={onSubmit}>
                <p className='font-weight-bold'>Old Password</p>
                <input
                  type='text'
                  value={userDetails.oldPassword}
                  name='userName'
                  readOnly
                />

                <p className='font-weight-bold'>New Password</p>
                <input
                  type='text'
                  name='displayName'
                  value={userDetails.newPassword}
                  onChange={onChange}
                />

                <p className='font-weight-bold'>Confirm Password</p>
                <input
                  type='text'
                  name='bio'
                  value={userDetails.confirmPassword}
                  onChange={onChange}
                />

                <input type='submit' value='Update' />
                <ValidationMessage
                  isVisible={formValidation.isVisible}
                  message={formValidation.message}
                  isValid={formValidation.isValid}
                />
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

ChangePassword.propTypes = {
  location: PropTypes.object.isRequired,
};

export default ChangePassword;
