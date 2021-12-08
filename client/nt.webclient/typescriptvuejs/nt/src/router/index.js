"use strict";
exports.__esModule = true;
var vue_1 = require("vue");
var vue_router_1 = require("vue-router");
var Shell_vue_1 = require("../pages/public/Shell.vue");
vue_1["default"].use(vue_router_1["default"]);
var routes = [
    {
        path: "/",
        name: "Home",
        component: Shell_vue_1["default"],
        meta: {
            requiresAuth: false
        }
    },
];
var router = new vue_router_1["default"]({
    routes: routes
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
exports["default"] = router;
