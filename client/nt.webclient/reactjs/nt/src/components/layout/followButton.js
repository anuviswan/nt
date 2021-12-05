import React from "react";
import PropTypes from "prop-types";

const FollowButton = ({ isFollowing, action }) => {
  const buttonText = isFollowing ? "Following" : "Follow Me";
  return (
    <input
      type='button'
      onClick={action}
      className='btn btn-primary block'
      value={buttonText}
    />
  );
};

FollowButton.protoType = {
  isFollowing: PropTypes.bool.isRequired,
  action: PropTypes.func.isRequired,
};
export default FollowButton;
