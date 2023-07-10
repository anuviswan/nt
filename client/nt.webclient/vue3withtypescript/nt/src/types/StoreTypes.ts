import { User } from "./UserTypes";

export default interface LoggedInUser extends User{
    token : string 
} 