import React, { useState } from "react";
import axios from "axios";

const RegisterUser = () => {
  const initialValue = {
    userName: "",
    password: "",
    displayName: "",
  };
  const [formData, updateFormData] = useState(initialValue);

  const onChange = (e) => {
    console.log(formData);

    updateFormData({ ...formData, [e.target.name]: e.target.value });
    console.log(formData);
  };
  const onSubmit = async (e) => {
    e.preventDefault();
    const headers = {
      "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
      "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
      "Content-Type": "application/json", // this shows the expected content type
    };

    const res = await axios.post(
      "https://localhost:44353/api/User/CreateUser",
      formData,
      {
        headers: headers,
      }
    );
    console.log(res);
  };
  return (
    <div className='container'>
      <div className='col-md-12 min-vh-100 d-flex flex-column justify-content-center'>
        <div className='row'>
          <div className='col-lg-6 col-md-8 mx-auto'>
            <div className='card rounded shadow shadow-sm'>
              <div className='card-header'>
                <h3 className='mb-0'>Sign Up</h3>
              </div>
              <div className='card-body'>
                <form className='form' onSubmit={onSubmit}>
                  <input
                    type='text'
                    name='userName'
                    placeholder='UserName'
                    onChange={onChange}
                  />
                  <input
                    type='text'
                    name='displayName'
                    placeholder='Display Name'
                    onChange={onChange}
                  />
                  <input
                    type='password'
                    name='password'
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

export default RegisterUser;
