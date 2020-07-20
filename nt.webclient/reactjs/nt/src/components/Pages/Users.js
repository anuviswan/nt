import React from "react";
import UserSearchCard from "../User/UserSearchCard";

const Users = ({ users }) => {
  return (
    <div>
      {users.map((user) => (
        <UserSearchCard key={users.username} user={user} />
      ))}
    </div>
  );
};

export default Users;
