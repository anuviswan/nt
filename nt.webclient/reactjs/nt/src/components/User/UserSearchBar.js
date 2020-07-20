import React, { useState } from "react";
import PropTypes from "prop-types";

const UserSearchBar = ({ searchUsers }) => {
  const [text, setText] = useState("");

  const onChange = (e) => {
    setText(e.target.value);
  };

  const onSubmit = (e) => {
    e.preventDefault();
    searchUsers(text);
    setText("");
  };

  return (
    <div>
      <form className='form' onSubmit={onSubmit}>
        <input
          type='text'
          name='text'
          placeholder='Search User'
          value={text}
          onChange={onChange}
        />
        <input type='submit' value='Search' className='btn-dark btn-block' />
      </form>
    </div>
  );
};

export default UserSearchBar;
