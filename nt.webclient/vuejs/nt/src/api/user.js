import axios from 'axios'
const validateUser = async (userName, passKey) =>{
  const userDetails = {
    userName: userName,
    passKey: btoa(passKey),
  };

  try{
    const response = await axios.post(
      "https://localhost:44353/api/User/ValidateUser",
      userDetails
    );
    return {
      data: response.data,
      hasError:false,
      error:[]
    }
  }
  catch(error){
    return {
      data:null,
      hasError:true,
      error:[error.response.data]
    }

  }
  
}

export {
    validateUser
}