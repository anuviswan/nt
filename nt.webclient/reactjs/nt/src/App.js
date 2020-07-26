import React from "react";
import logo from "./logo.svg";
import "./App.css";
import Navbar from "./components/layout/Navbar";
import Login from "./components/Pages/LoginAndRegisteration/Login";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import About from "./components/Pages/About";
import SearchUser from "./components/Pages/SearchUser";
import RegisterUser from "./components/Pages/LoginAndRegisteration/RegisterUser";
import UserState from "./context/user/UserState";
import UserContext from "./context/user/UserContext";

function App() {
  return (
    <UserState>
      <Router>
        <div className='App'>
          <Navbar title='November Talkies' icon='fa fa-film' />
          <Switch>
            <Route exact path='/' render={() => <Login />} />
            <Route exact path='/about' component={About} />
            <Route exact path='/searchuser' component={SearchUser} />
            <Route exact path='/signup' component={RegisterUser} />
          </Switch>
        </div>
      </Router>
    </UserState>
  );
}

export default App;
