interface IRouteInfo {
  name: string;
  path: string;
}
export interface IRouteNames {
  home: IRouteInfo;
  login: IRouteInfo;
  register: IRouteInfo;
  dashboard: IRouteInfo;
  changePassword: IRouteInfo;
  viewUserProfile: IRouteInfo;
  editUserProfile: IRouteInfo;
  searchPage: IRouteInfo;
}

const routesNames: Readonly<IRouteNames> = {
  home: {
    name: 'home',
    path: '/',
  },
  login: {
    name: 'login',
    path: '/login',
  },
  register: {
    name: 'register',
    path: '/register',
  },
  dashboard: {
    name: 'dashboard',
    path: '/p/dashboard',
  },
  changePassword: {
    name: 'changepassword',
    path: '/p/user',
  },
  viewUserProfile: {
    name: 'viewuserprofile',
    path: '/p/user/',
  },
  editUserProfile: {
    name: 'editUserProfile',
    path: '/p/user/edit',
  },
  searchPage: {
    name: 'searchPage',
    path: '/p/search',
  },
};

export default routesNames;
