import React from "react";
import UserSearchBar from "../User/UserSearchBar";
import axios from "axios";

const SearchUser = () => {
  const searchUsers = async (text) => {
    console.log(text);
  };
  return (
    <div>
      <div>
        <UserSearchBar searchUsers={searchUsers} />
      </div>
      <div>Search Result Page</div>
    </div>
  );
};

export default SearchUser;
