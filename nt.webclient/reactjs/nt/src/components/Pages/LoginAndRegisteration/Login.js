import React, { useState, useContext } from "react";
import axios from "axios";
import UserContext from "../../../context/user/UserContext";
import ValidationMessage from "../../layout/ValidationMessage";

const Login = () => {
  const userContext = useContext(UserContext);
  const initialProfileValue = {
    userName: "",
    passKey: "",
  };
  const initialValidationMsg = {
    message: "",
    isVisible: false,
    isValid: false,
  };
  const [userProfile, setLoginProfile] = useState(initialProfileValue);
  const [validationMsg, setValidationMsg] = useState(initialValidationMsg);

  const onChange = async (e) => {
    setLoginProfile({ ...userProfile, [e.target.name]: e.target.value });
  };

  const onSubmit = async (e) => {
    e.preventDefault();
    const headers = {
      "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
      "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
      "Content-Type": "application/json", // this shows the expected content type
    };

    const result = await axios.post(
      "https://localhost:44353/api/User/ValidateUser",
      userProfile,
      { headers: headers }
    );

    const { isAuthenticated, token, userName, errorMessage } = result.data;

    if (isAuthenticated) {
      console.log("Setting current User");
      userContext.validateUser({ authToken: token, userName: userName });
    } else {
      console.log("Setting Error");
      console.log(errorMessage);
      console.log({
        isVisible: true,
        isValid: false,
        message: errorMessage,
      });
      setValidationMsg({
        isVisible: true,
        isValid: false,
        message: errorMessage,
      });
    }
  };

  return (
    <div className='container'>
      <div className='col-md-12 min-vh-100 d-flex flex-column justify-content-center'>
        <div className='row'>
          <div className='col-lg-6 col-md-8 mx-auto'>
            <div className='card rounded shadow shadow-sm'>
              <div className='card-header'>
                <h3 className='mb-0'>Sign In</h3>
              </div>
              <div className='card-body'>
                <form className='form needs-validation' onSubmit={onSubmit}>
                  <input
                    type='text'
                    name='userName'
                    placeholder='UserName'
                    onChange={onChange}
                  />
                  <input
                    type='password'
                    name='passKey'
                    placeholder='Password'
                    onChange={onChange}
                  />
                  <input type='submit' value='Login' />
                  <ValidationMessage
                    isVisible={validationMsg.isVisible}
                    isValid={validationMsg.isValid}
                    message={validationMsg.message}
                  />
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Login;
