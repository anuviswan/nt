import React from "react";

const DynamicTextBox = ({ collection, defaultText }) => {
  const onChangeHandler = (e) => {};
  return (
    <div>
      {collection.map((row, index) => {
        console.log(row.text);
        return (
          <div className='row' key={index}>
            <input
              type='text'
              value={row.text}
              placeholder={defaultText}
              onChange={onChangeHandler}
            />
            asdasd
          </div>
        );
      })}
    </div>
  );
};

export default DynamicTextBox;
