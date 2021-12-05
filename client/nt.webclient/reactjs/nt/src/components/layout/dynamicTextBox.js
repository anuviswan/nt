import React, { useState } from "react";

const DynamicTextBox = ({
  collection,
  defaultText,
  buttonTitle,
  onCollectionChange,
}) => {
  const [currentCollection, setCurrentCollection] = useState(collection);
  const onChange = (e) => {
    const tempCollection = currentCollection;
    tempCollection[parseInt(e.target.name)] = { text: e.target.value };
    setCurrentCollection([...tempCollection]);
    onCollectionChange(currentCollection);
  };

  const onAddRow = (e) => {
    setCurrentCollection([...currentCollection, { text: "" }]);
    onCollectionChange(currentCollection);
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
              onChange={onChange}
            />
          </div>
        );
      })}
      <button type='button' className='btn btn-primary' onClick={onAddRow}>
        {buttonTitle}
      </button>
    </div>
  );
};

export default DynamicTextBox;
