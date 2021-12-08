import React, { useState, useContext } from "react";
import PropTypes from "prop-types";
import UserContext from "../../../context/user/userContext";
import ValidationMessage from "../../../components/layout/validationMessage";
import EditUserMenu, {
  EDIT_PROFILE,
} from "../../../components/User/editUserMenu";

import { updateUserProfile } from "../../../api/user";

const EditUser = ({ location }) => {
  const userContext = useContext(UserContext);
  const authToken = userContext.userToken;

  const [formValidation, setFormValidation] = useState({
    isVisible: false,
    isValid: true,
    message: "",
  });

  const { user } = location.state;
  const [userDetails, setUserDetails] = useState(user);

  const onChange = (e) => {
    setUserDetails({ ...userDetails, [e.target.name]: e.target.value });
  };

  const onSubmit = async (e) => {
    e.preventDefault();

    const response = await updateUserProfile(authToken, userDetails);
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
        message: ["User Details updated successfully"],
      });
    }
  };

  return (
    <div className='container-fluid '>
      <div className='row'>
        <div className='col-lg-2'>
          <EditUserMenu user={user} selected={EDIT_PROFILE} />
        </div>
        <div className='col-lg-10'>
          <div className='card'>
            <div className='card-header'>Edit User Profile</div>
            <div className='card-body'>
              <form className='form needs-validation' onSubmit={onSubmit}>
                <p className='font-weight-bold'>User Name</p>
                <input
                  type='text'
                  value={userDetails.userName}
                  name='userName'
                  readOnly
                />

                <p className='font-weight-bold'>Display Name</p>
                <input
                  type='text'
                  name='displayName'
                  value={userDetails.displayName}
                  onChange={onChange}
                />

                <p className='font-weight-bold'>Bio</p>
                <input
                  type='text'
                  name='bio'
                  value={userDetails.bio}
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

EditUser.propTypes = {
  location: PropTypes.object.isRequired,
};

export default EditUser;
