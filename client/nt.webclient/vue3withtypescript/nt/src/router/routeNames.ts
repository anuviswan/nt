export interface IRouteNames{
    home : string,

    login : string,
    register : string 
}

const routesNames: Readonly<IRouteNames> = {
    home: "home",
    login: "login",
    register: "register"
}

export default routesNames;