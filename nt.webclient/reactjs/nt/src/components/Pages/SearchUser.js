import React, { Component } from "react";
import UserSearchBar from "../User/UserSearchBar";
import axios from "axios";

class SearchUser extends Component {
  searchForUsers = async (text) => {
    const res = await axios.get(
      `https://localhost:44353/api/User/SearchUser?partialString=${text}`
    );
    this.setState({ users: res.data });
    console.log(this.state.users);
  };
  render() {
    return (
      <div>
        <div>
          <UserSearchBar searchUsers={this.searchForUsers} />
        </div>
        <div>TODO:Search Result Page</div>
      </div>
    );
  }
}

export default SearchUser;
