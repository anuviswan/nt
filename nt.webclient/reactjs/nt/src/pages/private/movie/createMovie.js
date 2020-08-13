import React, { useState, useContext } from "react";
import UserContext from "../../../context/user/userContext";
import Calender from "../../../components/layout/calender";
import DynamicTextBox from "../../../components/layout/dynamicTextBox";
import axios from "axios";
import ValidationMessage from "../../../components/layout/validationMessage";

const CreateMovie = () => {
  const currentUser = useContext(UserContext);
  const authToken = currentUser.userToken;

  const [formValidation, setFormValidation] = useState({
    isVisible: false,
    isValid: true,
    message: "",
  });

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
    let tempErrors = errors;
    // TODO : Validate
    if (movieMetadata.title === "") {
      tempErrors = [...tempErrors, "title"];
    }
    if (movieMetadata.language === "") {
      tempErrors = [...tempErrors, "language"];
    }

    setErrors(tempErrors);

    if (tempErrors.length > 0) {
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

      validateResponse(response.data.errorMessage);
    }
  };

  const validateResponse = (errorMessage) => {
    if (errorMessage) {
      setFormValidation({
        isValid: false,
        isVisible: true,
        message: errorMessage,
      });
    } else {
      setFormValidation({
        isValid: true,
        isVisible: true,
        message: "Movie created successfully",
      });
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
            <div className={hasError("title") ? "invalid-feedback" : "d-none"}>
              Please enter a movie title
            </div>
            <input
              type='text'
              name='language'
              className={
                hasError("language")
                  ? "form-control is-invalid"
                  : "form-control"
              }
              placeholder='Language'
              onChange={onChange}
            />
            <div
              className={hasError("language") ? "invalid-feedback" : "d-none"}
            >
              Please enter language
            </div>
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
            <ValidationMessage
              isVisible={formValidation.isVisible}
              message={formValidation.message}
              isValid={formValidation.isValid}
            />
          </form>
        </div>
      </div>
    </div>
  );
};

export default CreateMovie;
