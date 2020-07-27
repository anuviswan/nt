import React from "react";
import { Link } from "react-router-dom";
import Login from "./login";

const LandingPage = ({ icon, title }) => {
  return (
    <div className='container'>
      <div>
        <Login />
      </div>
    </div>
  );
};

export default LandingPage;
