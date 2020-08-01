import React from "react";
import PropTypes from "prop-types";

const EditUser = ({ location }) => {
  const { user } = location.state;

  console.log(user);
  return (
    <div className='container-fluid '>
      <div className='row'>
        <div className='col-lg-2'>
          <ul className='list-group'>
            <li className='list-group-item'>Edit User Profile</li>
            <li className='list-group-item'>Change Password</li>
          </ul>
        </div>
        <div className='col-lg-10'>
          <div className='card'>
            <div className='card-header'>Edit User Profile</div>
            <div className='card-body'>
              <form className='form needs-validation'>
                <p className='font-weight-bold'>User Name</p>
                <input
                  type='text'
                  value={user.userName}
                  name='userName'
                  readOnly
                />

                <p className='font-weight-bold'>Display Name</p>
                <input
                  type='text'
                  name='displayName'
                  value={user.displayName}
                  readOnly
                />

                <p className='font-weight-bold'>Bio</p>
                <input type='text' name='bio' value={user.bio} readOnly />

                <input type='submit' value='Update' />
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EditUser;
