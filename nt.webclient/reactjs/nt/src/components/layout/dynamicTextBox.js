import React, { useState } from "react";

const DynamicTextBox = ({ collection, defaultText, buttonTitle }) => {
  const [currentCollection, setCurrentCollection] = useState(collection);
  const onChangeHandler = (e) => {
    const tempCollection = currentCollection;
    tempCollection[parseInt(e.target.name)] = { text: e.target.value };
    setCurrentCollection([...tempCollection]);
  };

  const onAddRowClickHandler = (e) => {
    setCurrentCollection([...currentCollection, { text: "" }]);
  };
  return (
    <div>
      {currentCollection.map((row, index) => {
        return (
          <div key={index}>
            <input
              type='text'
              name={index}
              value={row.text}
              placeholder={defaultText}
              onChange={onChangeHandler}
            />
          </div>
        );
      })}
      <button
        type='button'
        className='btn btn-primary'
        onClick={onAddRowClickHandler}
      >
        {buttonTitle}
      </button>
    </div>
  );
};

export default DynamicTextBox;
