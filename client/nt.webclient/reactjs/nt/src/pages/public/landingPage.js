import React from "react";
import Login from "./login";

const LandingPage = ({ icon, title }) => {
  return (
    <div className='container-fluid'>
      <div className='col-md-12 min-vh-100 w-100 d-flex flex-column justify-content-center align-middle'>
        <div className='row'>
          <div className='col-lg-9 col-md-9 col-lx-9 mx-auto'>
            <h1>November Talkies</h1>
          </div>
          <div className='col-lg-3 col-md-3 col-lx-3 mx-auto'>
            <Login />
          </div>
        </div>
      </div>
    </div>
  );
};

export default LandingPage;
