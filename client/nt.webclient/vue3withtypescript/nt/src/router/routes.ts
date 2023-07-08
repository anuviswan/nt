import routesNames  from "./routeNames";
import { RouteRecordRaw } from 'vue-router'
import HomePage from '@/pages/public/HomePage.vue';
import RegisterPage from '@/pages/public/RegisterPage.vue'

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
  
  ]


export default routes;