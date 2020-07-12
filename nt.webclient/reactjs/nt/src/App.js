import React from "react";
import logo from "./logo.svg";
import "./App.css";
import Navbar from "./components/layout/Navbar";

function App() {
  return (
    <div className='App'>
      <Navbar title='November Talkies' icon='fa fa-github' />
    </div>
  );
}

export default App;
