interface IRouteInfo{
    name : string,
    path : string
}
export interface IRouteNames{
    home : IRouteInfo,
    login : IRouteInfo,
    register : IRouteInfo,
    dashboard : IRouteInfo,
    changePassword : IRouteInfo
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
    },
    dashboard:{
        name : 'dashboard',
        path : '/p/dashboard'
    },
    changePassword:{
        name : 'changepassword',
        path : '/p/user/'
    }
}

export default routesNames;