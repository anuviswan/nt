import {VuexModule, Module, Mutation, Action} from "vuex-module-decorators"
import {User} from "../../types/user";
@Module

export default class UserStore extends VuexModule{
    public currentUser:User ={
        UserName : '',
        DisplayName:''
    }        

    @Mutation
    public setCurrentUser(newUser:User):void{
        this.currentUser = newUser;
    }

    @Action
    public updateCurrentUser(newUser:User):void{
        this.context.commit('setCurrentUser',newUser);
    } 

}

