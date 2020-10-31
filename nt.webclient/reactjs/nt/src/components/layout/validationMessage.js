import React from "react";
import PropTypes from "prop-types";

const ValidationMessage = ({ isVisible, isValid, message }) => {
  console.log(message);
  if (isVisible) {
    const messageList = isValid
      ? message.map((msg) => <li className='valid-feedback d-block '>{msg}</li>)
      : message.map((msg) => (
          <li className='invalid-feedback d-block '>{msg}</li>
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
