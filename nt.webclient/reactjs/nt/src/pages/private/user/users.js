import React from "react";
import UserCard from "../../../components/User/userCard";

const Users = ({ users }) => {
  return (
    <div>
      {users.map((user) => (
        <UserCard key={users.username} user={user} />
      ))}
    </div>
  );
};

export default Users;
