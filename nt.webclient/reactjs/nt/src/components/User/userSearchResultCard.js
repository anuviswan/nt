import React from "react";
import { Link } from "react-router-dom";

const UserSearchResultCard = ({ user }) => {
  return (
    <div className='container'>
      <div className='card'>
        <img className='card-img-top' src='img_avatar1.png' alt='Avataar' />
        <div className='card-body'>
          <h4 className='card-title'>{user.displayName}</h4>
          <p className='card-text'>({user.userName})</p>
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

export default UserSearchResultCard;
