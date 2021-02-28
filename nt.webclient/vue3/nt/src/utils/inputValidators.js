export default function useValidator(value,validators){
        const errors = validators.map(validator=>validator(value));
        return errors.length==0;
}