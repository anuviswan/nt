import React, { useState, useContext } from "react";
import UserContext from "../../context/user/userContext";
import ValidationMessage from "../../components/layout/validationMessage";
import { NavLink } from "react-router-dom";
import { useNavigate  } from "react-router-dom";
import { validateUser } from "../../api/user";

const Login = () => {
  const history = useNavigate ();

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

    const result = await validateUser(
      userProfile.userName,
      userProfile.passKey
    );

    if (result.hasError) {
      console.log("login error");
      console.log(result);

      setValidationMsg({
        isVisible: true,
        isValid: false,
        message: result.error, //errorMessage,
      });
    } else {
      const {
        isAuthenticated,
        token,
        userName,
        displayName,
        bio,
      } = result.data;

      userContext.setCurrentUser({
        authToken: token,
        currentUser: {
          userName: userName,
          displayName: displayName,
          bio: bio,
          rating: 5,
        },
        isAuthenticated: isAuthenticated,
      });
      console.log(history);
      history("/home");
      console.log("Redirecting...");
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
          <NavLink to='/signup'>Sign up here</NavLink>
        </div>
      </div>
    </div>
  );
};

export default Login;
