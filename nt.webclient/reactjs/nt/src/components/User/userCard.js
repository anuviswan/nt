import React from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import Rating from "../layout/rating";
import FollowButton from "../layout/followButton";

const UserCard = ({ user, miniProfile, showUserName }) => {
  const followMeAction = () => {
    console.log("TBD");
  };
  const profile = miniProfile ? (
    <Link to='./User' className='btn btn-primary block'>
      View Profile
    </Link>
  ) : (
    <FollowButton isFollowing={false} action={followMeAction} />
  );

  const bio = miniProfile ? null : <p className='card-text'>{user.bio}</p>;

  const userName = showUserName ? (
    <p className='card-text text-secondary'>{user.userName}</p>
  ) : null;

  return (
    <div className='card border-primary mb-3'>
      <div className='text-right'>
        <Link to='/edituser'>
          <i className='fa fa-edit bg-light' aria-hidden='true'></i>
        </Link>
      </div>
      <div className='card-avataar'>
        <img
          className='card-img-top rounded-circle img-thumbnail'
          src='https://cdn3.iconfinder.com/data/icons/avatars-flat/33/woman_9-512.png'
          alt='Avataar Pic'
        />
      </div>

      <div className='card-body mx-auto'>
        <h4 className='card-title block text-uppercase text-center'>
          {user.displayName}
        </h4>
        {userName}
        <Rating value={user.rating} totalStars={5} />
      </div>

      <div className='card-header'>
        <div className='row'>
          <div className='col-lg-4 card-metadata text-center'>45</div>
          <div className='col-lg-4 card-metadata text-center'>345</div>
          <div className='col-lg-4 card-metadata text-center'>1.1K</div>
        </div>
        <div className='row'>
          <div className='col-lg-4 card-metadata-footer text-center'>
            Reviews
          </div>
          <div className='col-lg-4 card-metadata-footer text-center'>
            Followed
          </div>
          <div className='col-lg-4 card-metadata-footer text-center'>
            Followers
          </div>
        </div>
      </div>
      <div className='card-body mx-auto'>
        {bio}
        {profile}
      </div>
    </div>
  );
};

UserCard.propTypes = {
  user: PropTypes.object.isRequired,
  miniProfile: PropTypes.bool.isRequired,
};

export default UserCard;
