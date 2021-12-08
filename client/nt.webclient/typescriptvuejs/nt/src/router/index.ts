import Vue from "vue";
import VueRouter from "vue-router";
import Shell from "../pages/Shell.vue"
Vue.use(VueRouter);

const routes= [
  {
    path: "/",
    name: "Home",
    component: Shell,
    meta: {
      requiresAuth: false,
    },
  },
 
];

const router = new VueRouter({
  routes,
});

// router.beforeEach((to, from, next) => {
//   if (to.matched.some((record) => record.meta.requiresAuth)) {
//     if (users.actions.isAuthenticated()) {
//       next();
//     } else {
//       next("/");
//     }
//   } else {
//     next();
//   }
// });

export default router;
