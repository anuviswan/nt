import routesNames  from "./routeNames";
import { RouteRecordRaw } from 'vue-router'
import HomePage from '@/pages/public/HomePage.vue';

const routes: Array<RouteRecordRaw> = [
    {
      path: '/',
      name: routesNames.home,
      component: HomePage
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (about.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
    }
  ]


export default routes;