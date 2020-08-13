import React, { useState } from "react";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import PropTypes from "prop-types";

const Calender = ({ title, onChange, value }) => {
  const [selectedDate, setSelectedDate] = useState(value);

  const onCalendarClose = () => {
    onChange(selectedDate);
  };
  return (
    <div>
      <div>{title}</div>
      <DatePicker
        selected={selectedDate}
        onChange={(date) => setSelectedDate(date)}
        onCalendarClose={onCalendarClose}
        isClearable
        placeholderText={title}
      />
    </div>
  );
};

Calender.propTypes = {
  title: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  value: PropTypes.instanceOf(Date).isRequired,
};
export default Calender;
