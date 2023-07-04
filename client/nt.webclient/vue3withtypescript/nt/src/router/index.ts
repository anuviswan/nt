import { createRouter, createWebHashHistory, RouteRecordRaw } from 'vue-router'
import HomePage from '@/pages/public/HomePage.vue';
import routes from "./routes";




const router = createRouter({
  history: createWebHashHistory(),
  routes
})

export default router
