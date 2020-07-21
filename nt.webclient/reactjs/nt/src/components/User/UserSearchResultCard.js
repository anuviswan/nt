import React from "react";

const UserSearchResultCard = ({ user }) => {
  return (
    <div className='container'>
      <div className='card'>
        <img className='card-img-top' src='img_avatar1.png' alt='Card image' />
        <div className='card-body'>
          <h4 className='card-title'>{user.displayName}</h4>
          <p className='card-text'>{user.userName}</p>
          <a href='#' class='btn btn-primary'>
            See Profile
          </a>
        </div>
      </div>
    </div>
  );
};

export default UserSearchResultCard;
