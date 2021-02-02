export class ValidationError{
    public readonly Errors: Array<String>;

    constructor(errors:Array<string>){
        this.Errors = [];
    }

    /**
     * HasError : Returns true is has errors
     */
    public HasError() {
        return this.Errors.length;
    }
}