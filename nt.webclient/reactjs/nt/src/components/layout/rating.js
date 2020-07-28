import React from "react";
import PropTypes from "prop-types";

const Rating = ({ totalStars, value }) => {
  var stars = [];
  for (let i = 0; i < totalStars; i++)
    stars.push(
      i < value ? (
        <span className='fa fa-star text-warning'></span>
      ) : (
        <span className='fa fa-star' />
      )
    );
  return <span>{stars}</span>;
};

Rating.propTypes = {
  totalStars: PropTypes.number.isRequired,
  value: PropTypes.number.isRequired,
};

export default Rating;
