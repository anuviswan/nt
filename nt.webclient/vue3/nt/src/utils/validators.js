const minLength = min =>{
    return input => input.length < min ? `Invalid length : Minimum length expected is ${min}`:null;
}

const isEquals = (first,second) =>{
    return (first.normalize() === second.normalize())
}

export { minLength,isEquals};