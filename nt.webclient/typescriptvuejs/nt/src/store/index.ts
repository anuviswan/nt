import Vue from "vue";
import Vuex from "vuex"
import UserStore from "./module/user"

Vue.use(Vuex);

const store = new Vuex.Store({
    modules:{
        UserStore
    }
})

export default store;
