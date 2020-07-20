import React, { useState } from "react";
import UserSearchBar from "../User/UserSearchBar";
import axios from "axios";

const SearchUser = () => {
  const [searchResults, setSearchResults] = useState([]);

  const searchForUsers = async (text) => {
    const res = await axios.get(
      `https://localhost:44353/api/User/SearchUser?partialString=${text}`
    );
    setSearchResults((current) => res.data);
    console.log(searchResults);
  };

  return (
    <div>
      <div>
        <UserSearchBar searchUsers={searchForUsers} />
      </div>
      <div>TODO:Search Result Page</div>
    </div>
  );
};

export default SearchUser;
