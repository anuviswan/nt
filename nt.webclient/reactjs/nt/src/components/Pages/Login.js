import React, { Component } from "react";
import PropTypes from "prop-types";

const Login = () => {
  return (
    <div class='container'>
      <div class='col-md-12 min-vh-100 d-flex flex-column justify-content-center'>
        <div class='row'>
          <div class='col-lg-6 col-md-8 mx-auto'>
            <div class='card rounded shadow shadow-sm'>
              <div class='card-header'>
                <h3 class='mb-0'>Login</h3>
              </div>
              <div class='card-body'>
                <form className='form'>
                  <input type='text' name='username' placeholder='UserName' />
                  <input
                    type='password'
                    name='password'
                    placeholder='Password'
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
