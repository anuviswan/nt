import React, { useState } from "react";
import ValidationMessage from "../../components/layout/validationMessage";
import { registerUser } from "../../api/user";
const RegisterUser = () => {
  const initialValue = {
    userName: "",
    passKey: "",
    displayName: "",
  };

  const initialValidationStatus = {
    isVisible: false,
    isValid: true,
    message: "",
  };
  const [formData, updateFormData] = useState(initialValue);
  const [userNameValidation, updateUserValidation] = useState(
    initialValidationStatus
  );
  const [passwordValidation, updatePasswordValidation] = useState(
    initialValidationStatus
  );
  const [displayNameValidation, updatedisplayNameValidation] = useState(
    initialValidationStatus
  );
  const [formValidation, updateformValidation] = useState(
    initialValidationStatus
  );

  const onChange = (e) => {
    updateFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const validateUserName = (userName) => {
    if (!userName) {
      updateUserValidation({
        isVisible: true,
        isValid: false,
        message: ["UserName cannot be empty"],
      });
      return;
    }

    if (userName.length < 6) {
      updateUserValidation({
        isVisible: true,
        isValid: false,
        message: ["UserName should be minimum 6 characters"],
      });
      return;
    }

    if (/[^0-9a-zA-Z]/.test(userName)) {
      updateUserValidation({
        isVisible: true,
        isValid: false,
        message: ["Username can include only alphanumeric characters"],
      });
      return;
    }

    updateUserValidation({ isVisible: false, isValid: true, message: "" });
  };

  const validateDisplayName = (displayName) => {
    if (!displayName) {
      updatedisplayNameValidation({
        isVisible: true,
        isValid: false,
        message: ["Display Name cannot be empty"],
      });
      return;
    }

    if (displayName.length < 2) {
      updatedisplayNameValidation({
        isVisible: true,
        isValid: false,
        message: ["Display Name should be minimum 2 characters"],
      });
      return;
    }
    updatedisplayNameValidation({
      isVisible: false,
      isValid: true,
      message: [],
    });
  };

  const validatePassword = (password) => {
    if (!password) {
      updatePasswordValidation({
        isVisible: true,
        isValid: false,
        message: ["Password cannot be empty"],
      });
      return;
    }

    if (password.length < 6) {
      updatePasswordValidation({
        isVisible: true,
        isValid: false,
        message: ["Password should be minimum 6 characters"],
      });
      return;
    }
    updatePasswordValidation({ isVisible: false, isValid: true, message: "" });
  };

  const onSubmit = async (e) => {
    e.preventDefault();

    validateUserName(formData.userName);
    validateDisplayName(formData.displayName);
    validatePassword(formData.passKey);

    if (
      userNameValidation.isValid &&
      displayNameValidation.isValid &&
      passwordValidation.isValid
    ) {
      const response = await registerUser(
        formData.userName,
        formData.displayName,
        formData.passKey
      );

      console.log(response);

      if (!response.hasError) {
        updateformValidation({
          isVisible: true,
          isValid: true,
          message: ["User is successfully registered"],
        });
      } else {
        console.log(response.error);
        updateformValidation({
          isVisible: true,
          isValid: false,
          message: response.error,
        });
      }
    }
  };
  return (
    <div className='container-fluid'>
      <div className='col-md-12 min-vh-100 d-flex flex-column justify-content-center'>
        <div className='row'>
          <div className='col-lg-4 col-md-4 mx-auto'>
            <div className='card rounded shadow shadow-sm'>
              <div className='card-header bg-primary'>
                <h3 className='mb-0'>Register</h3>
              </div>
              <div className='card-body'>
                <form className='form needs-validation' onSubmit={onSubmit}>
                  <input
                    type='text'
                    name='userName'
                    placeholder='UserName'
                    onChange={onChange}
                  />
                  <ValidationMessage
                    isVisible={userNameValidation.isVisible}
                    message={userNameValidation.message}
                    isValid={userNameValidation.isValid}
                  />
                  <input
                    type='text'
                    name='displayName'
                    placeholder='Display Name'
                    onChange={onChange}
                  />
                  <ValidationMessage
                    isVisible={displayNameValidation.isVisible}
                    message={displayNameValidation.message}
                    isValid={displayNameValidation.isValid}
                  />
                  <input
                    type='password'
                    name='passKey'
                    placeholder='Password'
                    onChange={onChange}
                  />
                  <ValidationMessage
                    isVisible={passwordValidation.isVisible}
                    message={passwordValidation.message}
                    isValid={passwordValidation.isValid}
                  />
                  <input
                    type='submit'
                    value='Sign Up'
                    className='bg-primary btn-block'
                  />
                  <ValidationMessage
                    isVisible={formValidation.isVisible}
                    message={formValidation.message}
                    isValid={formValidation.isValid}
                  />
                </form>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default RegisterUser;
