import Vue from "vue";
import VueRouter from "vue-router";
import Home from "../pages/Home";
import Register from "../pages/public/Register";
import Dashboard from "../pages/private/Dashboard";
import users from "../store/modules/user";
import PrivateContainer from "../pages/private/PrivateContainer";
import ViewProfile from "../pages/private/user/ViewProfile";
import EditProfile from "../pages/private/user/EditProfile";
import ChangePassword from "../pages/private/user/ChangePassword";
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
        path: "user/:userid",
        component: ViewProfile,
      },
      {
        path: "user/:userid/edit",
        component: EditProfile,
      },
      {
        path: "user/:userid/chpwd",
        component: ChangePassword,
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
