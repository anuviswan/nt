import {LoginUser} from "../../types/loginuser"
import {ServerResponse} from "../../types/baseinterfaces"
export function SubmitForm(user:LoginUser):ServerResponse{
    
    return {HasError:false,Message:''};

}
