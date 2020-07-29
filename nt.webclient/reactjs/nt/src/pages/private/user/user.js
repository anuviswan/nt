import React from "react";
import UserCard from "../../../components/User/userCard";
const User = () => {
  return (
    <div className='row'>
      <div className='col-md-4 col-lg-2 col-lx-2'>
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
      <div className='col-md-8 col-lg-10 col-lx-10'>
        <div class='card'>
          <div class='card-header'>Movie Reviews</div>
          <div class='card-body'>
            <h5 class='card-title'>Dil Bechara</h5>
            <p class='card-text text-wrap'>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean
              nec placerat neque. Praesent ipsum sem, posuere at tortor sed,
              pharetra vestibulum metus. Suspendisse id porttitor tortor.
              Praesent efficitur ante lectus, a suscipit mi mollis eget. Sed eu
              purus vel mauris laoreet tempus eu non turpis. Nam in porttitor
              erat. Mauris non lorem mi. Praesent ac hendrerit quam.
            </p>
            <a href='#' class='btn btn-info'>
              more
            </a>
          </div>
          <div class='card-body'>
            <h5 class='card-title'>Life of Pie</h5>
            <p class='card-text'>
              Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean
              nec placerat neque. Praesent ipsum sem, posuere at tortor sed,
              pharetra vestibulum metus. Suspendisse id porttitor tortor.
              Praesent efficitur ante lectus, a suscipit mi mollis eget. Sed eu
              purus vel mauris laoreet tempus eu non turpis. Nam in porttitor
              erat. Mauris non lorem mi. Praesent ac hendrerit quam.
            </p>
            <a href='#' class='btn btn-info'>
              Go somewhere
            </a>
          </div>
        </div>
      </div>
    </div>
  );
};

export default User;
