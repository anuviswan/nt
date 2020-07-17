import React, { Component } from "react";
import PropTypes from "prop-types";

const Login = () => {
  return (
    <div>
      <form className='form'>
        <input type='text' name='username' placeholder='UserName' />
        <input type='password' name='password' placeholder='Password' />
        <input type='submit' value='Login' />
      </form>
    </div>
  );
};

export default Login;
