import React, { useContext, useState } from "react";
import PropTypes from "prop-types";
import UserContext from "../../../context/user/userContext";
import ValidationMessage from "../../../components/layout/validationMessage";
import EditUserMenu, {
  CHANGE_PWD,
} from "../../../components/User/editUserMenu";
import { changePassword } from "../../../api/user";

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

  const onChange = (e) => {
    setUserDetails({ ...userDetails, [e.target.name]: e.target.value });
  };
  const onSubmit = async (e) => {
    e.preventDefault();

    const response = await changePassword(
      authToken,
      userDetails.oldPassword,
      userDetails.newPassword
    );
    validateResponse(response);
  };

  const validateResponse = (response) => {
    if (response.hasError) {
      setFormValidation({
        isValid: false,
        isVisible: true,
        message: response.error,
      });
    } else {
      setFormValidation({
        isValid: true,
        isVisible: true,
        message: ["Password changed successfully"],
      });
    }
  };

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
                  type='password'
                  value={userDetails.oldPassword}
                  onChange={onChange}
                  name='oldPassword'
                />

                <p className='font-weight-bold'>New Password</p>
                <input
                  type='password'
                  name='newPassword'
                  value={userDetails.newPassword}
                  onChange={onChange}
                />

                <p className='font-weight-bold'>Confirm Password</p>
                <input
                  type='password'
                  name='confirmPassword'
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
