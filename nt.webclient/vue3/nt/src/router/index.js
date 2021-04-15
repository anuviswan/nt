import { createRouter, createWebHashHistory } from 'vue-router'
import Home from "../pages/public/Home"
import PrivateContainer from "@/pages/private/PrivateContainer"
import Dashboard from "@/pages/private/Dashboard"
import RegisterPage from "@/pages/public/RegisterPage"
import ViewProfile from "@/pages/private/user/ViewProfile.vue"
import ChangePassword from "@/pages/private/user/ChangePassword.vue"
const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/register',
    name: 'RegisterPage',
    component: RegisterPage
  },

  {
    path: "/p",
    name: "PrivateContainer",
    component: PrivateContainer,
    meta: {
      requiresAuth: true,
    },
    children: [
      {
        path: "dashboard",
        component: Dashboard,
      },
      {
        path:'user/:userid',
        component:ViewProfile
      },
      {
        path: "user/:userid/chpwd",
        component:ChangePassword
      }
    ]
  }
      
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
