import React, { useState } from "react";
import UserSearchBar from "../User/UserSearchBar";
import axios from "axios";

const SearchUser = () => {
  const [users, setUsers] = useState([{ displayName: "", userName: "" }]);

  const searchForUsers = async (text) => {
    const res = await axios.get(
      `https://localhost:44353/api/User/SearchUser?partialString=${text}`
    );
    setUsers(res.data);
    console.log(res.data);
    console.log(users);
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
