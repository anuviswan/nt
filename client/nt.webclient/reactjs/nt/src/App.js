import React from "react";
import "./App.css";
import Navbar from "./components/layout/navbar";
import Login from "./pages/public/login";
import { BrowserRouter as Router, Routes,Route } from "react-router-dom";
import About from "./pages/public/about";
import SearchUser from "./pages/private/user/searchUser";
import RegisterUser from "./pages/public/registerUser";
import UserState from "./context/user/userState";
import LandingPage from "./pages/public/landingPage";
import PrivateRoute from "./components/routes/privateRoute";
import Home from "./pages/private/home/home";
import User from "./pages/private/user/user";
import EditUser from "./pages/private/user/editUser";
import ChangePassword from "./pages/private/user/changePassword";
import CreateMovie from "./pages/private/movie/createMovie";
function App() {
  const privatePages = () => {
    return (
      <div>
        <Navbar title='November Talkies' icon='fa fa-film' />
        <Routes>
          <Route path='/searchuser' element={<PrivateRoute><SearchUser/></PrivateRoute>} />
          <Route path='/home' element={<PrivateRoute><Home/></PrivateRoute>}/>
          <Route path='/user' element={<PrivateRoute><User/></PrivateRoute>} />
          <Route path='/edituser' element={<PrivateRoute><EditUser/></PrivateRoute>} />
          <Route path='/createmovie' element={<PrivateRoute><CreateMovie/></PrivateRoute>} />
          <Route path='/changepassword' element={<PrivateRoute><ChangePassword/></PrivateRoute>} />
        </Routes>
      </div>
    );
  };
  return (
    <UserState>
      <Router>
        <div className='App'>
          <Routes>
            <Route exact path='/' element={<LandingPage/>} />
            <Route exact path='/About' element={<About/>} />
            <Route exact path='/signup' element={<RegisterUser/>} />
            <Route exact path='/signin' element={<Login/>} />

            <Route element={privatePages} />
          </Routes>
        </div>
      </Router>
    </UserState>
  );
}

export default App;
