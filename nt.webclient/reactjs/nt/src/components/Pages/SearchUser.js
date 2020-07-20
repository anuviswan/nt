import React, { Component } from "react";
import UserSearchBar from "../User/UserSearchBar";
import axios from "axios";
import { render } from "@testing-library/react";

class SearchUser extends Component {
  state = {
    users: {},
  };
  searchForUsers = async (text) => {
    const res = await axios.get(
      `https://localhost:44353/api/User/SearchUser?partialString=${text}`
    );
    this.setState({ users: res.data });
    console.log(res.data);
    console.log(this.users);
  };
  render() {
    return (
      <div>
        <div>
          <UserSearchBar searchUsers={this.searchForUsers} />
        </div>
        <div>Search Result Page</div>
      </div>
    );
  }
}

export default SearchUser;
