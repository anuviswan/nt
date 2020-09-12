import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../pages/Home";
import Register from "../pages/public/Register";
import Dashboard from "../pages/private/Dashboard";
import users from "../store/modules/user";
import PrivateContainer from "../pages/private/PrivateContainer";
import ViewProfile from "../pages/private/user/ViewProfile";
Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Home",
    component: Home,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/register",
    name: "Register",
    component: Register,
    meta: {
      requiresAuth: false,
    },
  },
  {
    path: "/about",
    name: "About",
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () =>
      import(/* webpackChunkName: "about" */ "../views/About.vue"),
    meta: {
      requiresAuth: false,
    },
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
        path: "user",
        component: ViewProfile,
      },
    ],
  },
];

const router = new VueRouter({
  routes,
});

router.beforeEach((to, from, next) => {
  if (to.matched.some((record) => record.meta.requiresAuth)) {
    if (users.actions.isAuthenticated()) {
      next();
    } else {
      next("/");
    }
  } else {
    next();
  }
});

export default router;
