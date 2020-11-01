import React from "react";
import UserCard from "../../../components/User/userCard";

const Users = ({ users }) => {
  if (users == null) {
    return <div></div>;
  } else if (users.length === 0) {
    return (
      <div className='alert alert-info' role='alert'>
        No users found !!
      </div>
    );
  }
  return (
    <div style={userStyle}>
      {users.map((user, index) => (
        <UserCard key={index} user={user} canViewFullProfile={false} />
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
