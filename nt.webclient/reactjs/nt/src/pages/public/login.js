import React, { useState, useContext } from "react";
import axios from "axios";
import UserContext from "../../context/user/userContext";
import ValidationMessage from "../../components/layout/validationMessage";
import { Link } from "react-router-dom";
import { Base64 } from "js-base64";
import { useHistory } from "react-router-dom";

const Login = () => {
  const history = useHistory();

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

    const base64PassKey = Base64.encode(userProfile.passKey);
    setLoginProfile({
      ...userProfile,
      passKey: base64PassKey,
    });

    console.log(base64PassKey);

    setLoginProfile({ ...userProfile, passKey: base64PassKey });

    const result = await axios.post(
      "https://localhost:44353/api/User/ValidateUser",
      { ...userProfile, passKey: base64PassKey },
      { headers: headers }
    );

    console.log(result.data);

    const {
      isAuthenticated,
      token,
      userName,
      errorMessage,
      displayName,
      bio,
    } = result.data;

    if (isAuthenticated) {
      console.log("User Authenticated");
      userContext.validateUser({
        authToken: token,
        userName: userName,
        isAuthenticated: isAuthenticated,
        displayName: displayName,
        rating: 5,
        bio: bio,
      });
      console.log(history);
      history.push("/home");
      console.log("Redirecting...");
    } else {
      setValidationMsg({
        isVisible: true,
        isValid: false,
        message: errorMessage,
      });
    }
  };

  return (
    <div className='card rounded shadow shadow-sm'>
      <div className='card-header bg-primary'>
        <div className='card-title'>
          <h3 className='mb-0 '>Sign In</h3>
        </div>
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
          <input type='submit' value='Login' className='bg-primary btn-block' />
          <ValidationMessage
            isVisible={validationMsg.isVisible}
            isValid={validationMsg.isValid}
            message={validationMsg.message}
          />
        </form>
        <div>
          <Link to='/signup'>Sign up here</Link>
        </div>
      </div>
    </div>
  );
};

export default Login;
