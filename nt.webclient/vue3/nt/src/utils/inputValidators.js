
export default function inputValidator(value,validators,action){
        const error = validators.map(validator=>validator(value));
        action(error);
}