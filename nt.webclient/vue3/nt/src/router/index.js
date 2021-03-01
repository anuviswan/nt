import { createRouter, createWebHashHistory } from 'vue-router'
import Home from "../pages/public/Home"
import PrivateContainer from "@/pages/private/PrivateContainer"
import Dashboard from "@/pages/private/Dashboard"
const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
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
    ]
  }
      
]

const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
