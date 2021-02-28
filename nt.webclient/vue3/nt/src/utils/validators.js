const minLength = min =>{
    return input => input.length < min ? `invalid length : ${min}`:null;
}

export { minLength};