import React from "react";
const EditUser = () => {
  return (
    <div className='container'>
      <div className='row'>
        <div>
          <ul class='list-group'>
            <li class='list-group-item'>Edit User Profile</li>
            <li class='list-group-item'>Change Password</li>
          </ul>
        </div>
        <div className='col-lg-8'>
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
