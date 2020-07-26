import React, { useState } from "react";
import axios from "axios";

const Login = () => {
  const initialProfileValue = {
    userName: "",
    passKey: "",
  };
  const [userProfile, setLoginProfile] = useState(initialProfileValue);

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
    console.log(result);
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
