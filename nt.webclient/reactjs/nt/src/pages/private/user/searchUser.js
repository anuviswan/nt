import React, { useState, useContext } from "react";
import UserSearchBar from "../../../components/User/userSearchBar";
import Users from "./users";
import { searchUserList } from "../../../api/user";
import UserContext from "../../../context/user/userContext";

const SearchUser = () => {
  const [searchResults, setSearchResults] = useState([]);
  const userContext = useContext(UserContext);
  const authToken = userContext.userToken;

  const searchForUsers = async (authToken, text) => {
    const res = await searchUserList(authToken, text);
    setSearchResults((current) => res.data);
    console.log(searchResults);
  };

  return (
    <div>
      <div>
        <UserSearchBar searchUsers={searchForUsers} authToken={authToken} />
      </div>
      <div>
        <Users users={searchResults} />
      </div>
    </div>
  );
};

export default SearchUser;
