const minLength = min =>{
    return input => input.length < min ? `Invalid length : Minimum length expected is ${min}`:null;
}

export { minLength};