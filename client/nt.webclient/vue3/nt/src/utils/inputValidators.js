export default function useValidator(value,validators,onErrorAction,onValidAction){
        const validationResults = validators.map(validator=>{
                return validator(value);
        });
        
        const errors = validationResults.filter(x=>x!=null);
        if(errors.length!=0){
                onErrorAction(errors);
        }
        else{
                if(onValidAction != null)
                        onValidAction();
        }
        return errors;
}
       