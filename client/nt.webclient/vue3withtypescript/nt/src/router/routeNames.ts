interface IRouteInfo{
    name : string,
    path : string
}
export interface IRouteNames{
    home : IRouteInfo,
    login : IRouteInfo,
    register : IRouteInfo 
}

const routesNames: Readonly<IRouteNames> = {
    home: { 
        name: "home",
        path :'/'
    },
    login: {
        name : 'login',
        path : '/login'
    },
    register: {
        name : 'register',
        path : '/register'
    }
}

export default routesNames;