import React from "react";
import logo from "./logo.svg";
import "./App.css";
import Navbar from "./components/layout/Navbar";
import Login from "./components/Pages/Login";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import About from "./components/Pages/About";

function App() {
  return (
    <Router>
      <div className='App'>
        <Navbar title='November Talkies' icon='fa fa-github' />
        <Switch>
          <Route exact path='/' render={() => <Login />} />
          <Route exact path='/about' component={About} />
        </Switch>
      </div>
    </Router>
  );
}

export default App;
