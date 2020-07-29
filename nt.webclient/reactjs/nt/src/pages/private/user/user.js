import React from "react";
import UserCard from "../../../components/User/userCard";
const User = () => {
  return (
    <div className='row'>
      <div className='col-md-6 col-lg-2 col-lx-2'>
        <UserCard
          user={{
            displayName: "Jia Anu",
            userName: "jiaanu",
            bio: "Hey, am jia kutty",
            rating: 4,
          }}
          miniProfile={false}
        />
      </div>
      <div className='col-md-6 col-lg-2 col-lx-2'>User Profile</div>
    </div>
  );
};

export default User;
