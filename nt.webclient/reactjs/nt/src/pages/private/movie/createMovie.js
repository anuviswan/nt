import React, { useState } from "react";
import Calender from "../../../components/layout/calender";
import DynamicTextBox from "../../../components/layout/dynamicTextBox";

const CreateMovie = () => {
  const [movieMetadata, setMovieMetadata] = useState({
    title: "",
    language: "",
    releaseDate: new Date(),
  });

  const onChange = (e) => {
    setMovieMetadata({ ...movieMetadata, [e.target.name]: e.target.value });
  };

  const onReleaseDateChange = (date) => {
    setMovieMetadata({ ...movieMetadata, releaseDate: date });
  };

  const onSubmit = (e) => {
    e.preventDefault();
    console.log(movieMetadata);
  };
  return (
    <div className='container-fluid'>
      <div className='row'>
        <div className='col-lg-12'>
          <form onSubmit={onSubmit}>
            <input
              type='text'
              name='title'
              placeholder='Movie Title'
              onChange={onChange}
            />
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
