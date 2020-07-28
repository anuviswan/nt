import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";

const UserCard = ({ user }) => {
  return (
    <div className='container'>
      <div className='card'>
        <img
          className='card-img-top rounded-circle img-thumbnail'
          src='https://cdn3.iconfinder.com/data/icons/avatars-flat/33/woman_9-512.png'
          alt='Avataar Pic'
        />
        <div className='card-body justify-content-center'>
          <h4 className='card-title'>{user.displayName}</h4>
          <p className='card-text text-secondary'>{user.userName}</p>
          <div className='row bg-info'>
            <div className='py2'>Followed</div>
            <div className='py2'>Followed</div>
            <div className='py2'>Followed</div>
          </div>
          <p className='card-text'>{user.bio}</p>
          <p className='card-text'>Rating:{user.rating}</p>
          <Link to='./User' class='btn btn-primary'>
            See Profile
          </Link>
        </div>
      </div>
    </div>
  );
};

UserCard.propTypes = {
  user: PropTypes.object.isRequired,
};

export default UserCard;
