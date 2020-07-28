import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import Rating from "../layout/rating";

const UserCard = ({ user, showProfile, showUserName }) => {
  const profile = showProfile ? (
    <Link to='./User' class='btn btn-primary'>
      See Profile
    </Link>
  ) : null;

  const userName = showUserName ? (
    <p className='card-text text-secondary'>{user.userName}</p>
  ) : null;
  return (
    <div className='card border-primary mb-3'>
      <div className='card-avataar'>
        <img
          className='card-img-top rounded-circle img-thumbnail'
          src='https://cdn3.iconfinder.com/data/icons/avatars-flat/33/woman_9-512.png'
          alt='Avataar Pic'
        />
      </div>
      <div className='card-body '>
        <h4 className='card-title block'>{user.displayName}</h4>
        {userName}
        <Rating value={user.rating} totalStars='5' />
      </div>
      <div className='card-header'>
        <div className='row'>
          <div className='col-lg-4 py2'>Followed</div>
          <div className='col-lg-4 py2'>Followed</div>
          <div className='col-lg-4 py2'>Followed</div>
        </div>
      </div>
      <p className='card-text'>{user.bio}</p>
    </div>
  );
};

UserCard.propTypes = {
  user: PropTypes.object.isRequired,
};

export default UserCard;
