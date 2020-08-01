import React, { useEffect } from "react";
import { Link } from "react-router-dom";
import PropTypes from "prop-types";
import Rating from "../layout/rating";
import FollowButton from "../layout/followButton";

const UserCard = ({ user, canEdit, canViewFullProfile }) => {
  useEffect(() => {
    getUpdatedUserRating();
  }, []);

  // Retrieve updated user rating
  const getUpdatedUserRating = async () => {
    console.log("TDB : getUpdatedUserRating");
  };

  const followMeAction = () => {
    console.log("TBD");
  };

  // check if View Profile button can be seen
  const buttonUI = canViewFullProfile ? (
    <Link to='./User' className='btn btn-primary block'>
      View Profile
    </Link>
  ) : (
    <FollowButton isFollowing={false} action={followMeAction} />
  );

  // Check if Editable
  const editUserUI = canEdit ? (
    <div className='text-right'>
      <Link to={{ pathname: "/edituser", state: { user: user } }}>
        <i className='fa fa-edit bg-light' aria-hidden='true' />
      </Link>
    </div>
  ) : null;

  // Check if Bio can be viewed
  const bioUI = canViewFullProfile ? null : (
    <p className='card-text'>{user.bio}</p>
  );

  // Check if userName can be viewed
  const userNameUI = canViewFullProfile ? (
    <p className='card-text text-secondary'>{user.userName}</p>
  ) : null;

  return (
    <div className='card border-primary mb-3'>
      {editUserUI}
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
        {userNameUI}
        <Rating value={5} totalStars={5} />
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
        {bioUI}
        {buttonUI}
      </div>
    </div>
  );
};

UserCard.propTypes = {
  user: PropTypes.object.isRequired,
  canViewFullProfile: PropTypes.bool.isRequired,
};

export default UserCard;
