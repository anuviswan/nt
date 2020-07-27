import React from "react";
import UserSearchResultCard from "../../../components/User/userSearchResultCard";

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
