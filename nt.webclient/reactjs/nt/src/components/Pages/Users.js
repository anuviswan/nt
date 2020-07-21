import React from "react";
import UserSearchResultCard from "../User/UserSearchResultCard";

const Users = ({ users }) => {
  return (
    <div>
      {users.map((user) => (
        <UserSearchResultCard key={users.username} user={user} />
      ))}
    </div>
  );
};

export default Users;
