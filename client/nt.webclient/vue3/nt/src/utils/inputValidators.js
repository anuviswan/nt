export default function useValidator(value,validators,onErrorAction){
        const validationResults = validators.map(validator=>{
                return validator(value);
        });
        
        const errors = validationResults.filter(x=>x!=null);
        if(errors.length!=0){
        onErrorAction(errors);
        }
        return errors;
}
       