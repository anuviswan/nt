import React from "react";
const EditUser = () => {
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
            <div className='card-header'>Featured</div>
            <div className='card-body'>
              <form className='form needs-validation'>
                <p className='font-weight-bold'>User Name</p>
                <input
                  type='text'
                  name='userName'
                  placeholder='UserName'
                  readonly
                />

                <p className='font-weight-bold'>Display Name</p>
                <input type='text' name='userName' placeholder='UserName' />

                <p className='font-weight-bold'>Bio</p>
                <input type='text' name='userName' placeholder='UserName' />
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default EditUser;
