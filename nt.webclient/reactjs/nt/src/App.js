import React from "react";
import "./App.css";
import Navbar from "./components/layout/navbar";
import Login from "./pages/public/login";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import About from "./pages/public/about";
import SearchUser from "./pages/private/user/searchUser";
import RegisterUser from "./pages/public/registerUser";
import UserState from "./context/user/userState";
import LandingPage from "./pages/public/landingPage";
import PrivateRoute from "./components/routes/privateRoute";
import PublicRoute from "./components/routes/publicRoute";
import Home from "./pages/private/home/home";
import User from "./pages/private/user/user";

function App() {
  const privatePages = () => {
    return (
      <div>
        <Navbar title='November Talkies' icon='fa fa-film' />
        <Switch>
          <PrivateRoute exact path='/searchuser' component={SearchUser} />
          <PrivateRoute exact path='/home' component={Home} />
        </Switch>
      </div>
    );
  };
  return (
    <UserState>
      <Router>
        <div className='App'>
          <Switch>
            <PublicRoute exact path='/' component={LandingPage} />
            <PublicRoute exact path='/About' component={About} />
            <PublicRoute exact path='/signup' component={RegisterUser} />
            <PublicRoute exact path='/signin' component={Login} />
            <PublicRoute exact path='/user' component={User} />
            <Route component={privatePages} />
          </Switch>
        </div>
      </Router>
    </UserState>
  );
}

export default App;
