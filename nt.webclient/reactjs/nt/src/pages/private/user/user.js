import React from "react";
import UserCard from "../../../components/User/userCard";
import { Link } from "react-router-dom";
const User = ({ location }) => {
  console.log(location.state);
  const { user } = location.state;

  return (
    <div className='row'>
      <div className='col-md-4 col-lg-2 col-lx-2'>
        <UserCard
          user={{
            displayName: user.userName,
            userName: user.userName,
            bio: "Hey, am jia kutty",
            rating: 4,
          }}
          miniProfile={false}
        />
      </div>
      <div className='col-md-8 col-lg-10 col-lx-10'>
        <div className='card'>
          <div className='card-header'>Movie Reviews</div>
          <div className='card-body'>
            <h5 className='card-title'>Dil Bechara</h5>
            <p className='card-text text-wrap'>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean
              nec placerat neque. Praesent ipsum sem, posuere at tortor sed,
              pharetra vestibulum metus. Suspendisse id porttitor tortor.
              Praesent efficitur ante lectus, a suscipit mi mollis eget. Sed eu
              purus vel mauris laoreet tempus eu non turpis. Nam in porttitor
              erat. Mauris non lorem mi. Praesent ac hendrerit quam.
            </p>
            <Link to='/more' className='btn btn-info'>
              more
            </Link>
          </div>
          <div className='card-body'>
            <h5 className='card-title'>Life of Pie</h5>
            <p className='card-text'>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean
              nec placerat neque. Praesent ipsum sem, posuere at tortor sed,
              pharetra vestibulum metus. Suspendisse id porttitor tortor.
              Praesent efficitur ante lectus, a suscipit mi mollis eget. Sed eu
              purus vel mauris laoreet tempus eu non turpis. Nam in porttitor
              erat. Mauris non lorem mi. Praesent ac hendrerit quam.
            </p>
            <Link to='/more' className='btn btn-info'>
              more
            </Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default User;
