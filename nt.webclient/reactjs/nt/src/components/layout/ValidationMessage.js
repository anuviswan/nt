import React from "react";
import PropTypes from "prop-types";

const ValidationMessage = ({ isVisible, isValid, message }) => {
  if (isVisible) {
    return isValid ? (
      <div className='valid-feedback d-block '>{message}</div>
    ) : (
      <div className='invalid-feedback d-block '>{message}</div>
    );
  }

  return null;
};

ValidationMessage.protoType = {
  isVisible: PropTypes.bool.isRequired,
  isValid: PropTypes.bool.isRequired,
  message: PropTypes.string,
};

export default ValidationMessage;
