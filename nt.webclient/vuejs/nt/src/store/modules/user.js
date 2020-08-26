//import axios from "axios";

const state = {
  currentUser: {},
};

const getters = {
  currentUser: (state) => state.currentUser,
};

const actions = {
  updateCurrentUser({ commit }, user) {
    console.log(user);
    commit("updateCurrentUser", user);
  },
};

const mutations = {
  updateCurrentUser: (state, user) => (state.currentUser = user),
};

export default {
  state,
  getters,
  actions,
  mutations,
};
