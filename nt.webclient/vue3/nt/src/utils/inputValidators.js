export default function useValidator(value,validators){
        const errors = validators.map(validator=>{
                return validator(value) == null;
        });

        return errors.includes(false) == false;
}