const minLength = min =>{
    return input => input.length < min ? `Invalid length : Minimum length expected is ${min}`:null;
}

const isEquals = (toCompare) =>{
    return input => (toCompare.normalize() === input.normalize()) ? null : `Values do not match`;
}

export { minLength,isEquals};