import routesNames  from "./routeNames";
import { RouteRecordRaw } from 'vue-router'
import HomePage from '@/pages/public/HomePage.vue';
import RegisterPage from '@/pages/public/RegisterPage.vue'
import DashboardPage from '@/pages/private/DashboardPage.vue'
import ChangePassword from '@/pages/private/user/ChangePassword.vue'
import PrivateContainer from '@/pages/private/PrivateContainer.vue' 
import ViewProfile from "@/pages/private/user/ViewProfile.vue"
import EditUser from  "@/pages/private/user/EditUser.vue"

const routes: Array<RouteRecordRaw> = [
    {
      path: routesNames.home.path,
      name: routesNames.home.name,
      component: HomePage
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
    },

    {
      path: routesNames.register.path,
      name: routesNames.register.name,
      component: RegisterPage
    },
    {
      path: "/p",
      name: "PrivateContainer",
      component: PrivateContainer,
      meta :{
        requiresAuth : true,
      },
      children:[
        {
          path: routesNames.dashboard.path,
          name : routesNames.dashboard.name,
          component : DashboardPage
        },
        {
          
          name : routesNames.changePassword.name,
          path: routesNames.changePassword.path + '/:userid/chpwd',
          component : ChangePassword
        },
        {
          name: routesNames.viewUserProfile.name,
          path: routesNames.viewUserProfile.path + '/:userid/view',
          component : ViewProfile
        },
        {
          name: routesNames.editUserProfile.name,
          path: routesNames.editUserProfile.path,
          component : EditUser
        }
      ]
    }
    
  
  ]


export default routes;