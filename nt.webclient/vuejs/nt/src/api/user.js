import axios from 'axios'

// Validate User
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

const getUser = async (userName)=>{
  const params =  {
    params:{
    userName : userName
    }
  };

  try{
    var response = await axios.get("https://localhost:44353/api/User/GetUser", params);
    return {
      data: response.data,
      hasError:false,
      error:[]
    }
  }
  catch(error){
    if(error.response.status==400){
      return {
        data:null,
        hasError:true,
        error:[error.response.data]
      }
    }
    


  }
  

}

export {
    validateUser,
    getUser
}