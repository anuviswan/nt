import React, { useState } from "react";
import UserSearchBar from "../../../components/User/userSearchBar";
import Users from "./users";
import { searchUserList } from "../../../api/user";

const SearchUser = () => {
  const [searchResults, setSearchResults] = useState([]);

  const searchForUsers = async (text) => {
    const res = await searchUserList(text);
    setSearchResults((current) => res.data);
    console.log(searchResults);
  };

  return (
    <div>
      <div>
        <UserSearchBar searchUsers={searchForUsers} />
      </div>
      <div>
        <Users users={searchResults} />
      </div>
    </div>
  );
};

export default SearchUser;
