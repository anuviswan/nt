import React, { useState, useContext } from "react";
import PropTypes from "prop-types";
import Axios from "axios";
import UserContext from "../../../context/user/userContext";
import ValidationMessage from "../../../components/layout/validationMessage";
import EditUserMenu, {
  EDIT_PROFILE,
} from "../../../components/User/editUserMenu";

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

    const headers = {
      "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
      "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
      "Content-Type": "application/json", // this shows the expected content type
      Authorization: `Bearer ${authToken}`,
    };

    const response = await Axios.post(
      "https://localhost:44353/api/User/UpdateUser",
      userDetails,
      { headers: headers }
    );

    validateResponse(response.data.errorMessage);
  };

  const validateResponse = (errorMessage) => {
    if (errorMessage) {
      setFormValidation({
        isValid: false,
        isVisible: true,
        message: errorMessage,
      });
    } else {
      setFormValidation({
        isValid: true,
        isVisible: true,
        message: "User Details updated successfully",
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
