import React from "react";
import PropTypes from "prop-types";

const ValidationMessage = ({ isVisible, isValid, message }) => {
  if (isVisible) {
    const messageList = isValid
      ? message.map((msg, index) => (
          <li className='valid-feedback d-block ' key={index}>
            {msg}
          </li>
        ))
      : message.map((msg, index) => (
          <li className='invalid-feedback d-block ' key={index}>
            {msg}
          </li>
        ));

    return <ul>{messageList}</ul>;
  }

  return null;
};

ValidationMessage.protoType = {
  isVisible: PropTypes.bool.isRequired,
  isValid: PropTypes.bool.isRequired,
  message: PropTypes.array,
};

export default ValidationMessage;
