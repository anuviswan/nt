import React, { useState, useContext } from "react";
import UserContext from "../../../context/user/userContext";
import Calender from "../../../components/layout/calender";
import DynamicTextBox from "../../../components/layout/dynamicTextBox";
import axios from "axios";

const CreateMovie = () => {
  const currentUser = useContext(UserContext);
  const authToken = currentUser.userToken;

  const [movieMetadata, setMovieMetadata] = useState({
    title: "",
    language: "",
    releaseDate: new Date(),
    actors: [],
  });

  const [errors, setErrors] = useState([]);

  const onChange = (e) => {
    setMovieMetadata({ ...movieMetadata, [e.target.name]: e.target.value });
  };

  const onReleaseDateChange = (date) => {
    setMovieMetadata({ ...movieMetadata, releaseDate: date });
  };

  const onActorCollectionChange = (newCollection) => {
    console.log(newCollection);
    setMovieMetadata({
      ...movieMetadata,
      actors: newCollection.map((actor) => actor.text),
    });
  };

  const hasError = (key) => errors.indexOf(key) !== -1;

  const onSubmit = async (e) => {
    e.preventDefault();
    console.log(errors);
    setErrors([]);
    // TODO : Validate
    if (movieMetadata.title === "") {
      console.log("has title eror");
      setErrors([...errors, "title"]);
      console.log(errors);
    }
    if (movieMetadata.language === "") {
      setErrors([...errors, "language"]);
    }

    console.log(errors);

    if (errors.length === 0) {
      return false;
    } else {
      const headers = {
        "Access-Control-Allow-Headers": "*", // this will allow all CORS requests
        "Access-Control-Allow-Methods": "OPTIONS,POST,GET", // this states the allowed methods
        "Content-Type": "application/json", // this shows the expected content type
        Authorization: `Bearer ${authToken}`,
      };

      const response = await axios.post(
        "https://localhost:44353/api/Movie/CreateMovie",
        movieMetadata,
        { headers: headers }
      );
    }
  };
  return (
    <div className='container-fluid'>
      <div className='row'>
        <div className='col-lg-12'>
          <form onSubmit={onSubmit}>
            <input
              type='text'
              name='title'
              className={
                hasError("title") ? "form-control is-invalid" : "form-control"
              }
              placeholder='Movie Title'
              onChange={onChange}
            />
            <div className={hasError("title") ? "inline-errormsh" : "hidden"}>
              Please enter a movie title
            </div>
            <input
              type='text'
              name='language'
              placeholder='Language'
              onChange={onChange}
            />
            <Calender
              title='Release Date'
              onChange={(date) => onReleaseDateChange(date)}
              value={movieMetadata.releaseDate}
            />
            <DynamicTextBox
              defaultText='Actor'
              collection={[{ text: "Aamir Khan" }]}
              onCollectionChange={onActorCollectionChange}
              buttonTitle='Add Actor'
            />

            <input type='submit' value='Create Movie' />
          </form>
        </div>
      </div>
    </div>
  );
};

export default CreateMovie;
