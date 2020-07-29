import React from "react";
import UserCard from "../../../components/User/userCard";

const Users = ({ users }) => {
  return (
    <div style={userStyle}>
      {users.map((user, index) => (
        <UserCard key={index} user={user} miniProfile={true} />
      ))}
    </div>
  );
};

const userStyle = {
  display: "grid",
  gridTemplateColumns: "repeat(5, 1fr)",
  gridGap: "1rem",
};

export default Users;
