import {authService} from '../services';
import  {router} from '../router';

const user = JSON.parse(localStorage.getItem('user'));
const state = user
    ? { status: { loggedIn: true }, user }
    : { status: {}, user: null };

const actions = {
  login({dispatch, commit}, {username, password}) {
      commit('loginRequest', {username});

      authService.login(username, password)
          .then(
              user => {
                  commit('loginSuccess', user);
                  router.push('/');
              },
              error => {
                  commit('loginFailure', error);
                  dispatch('alert/error', error, {root: true});
              }
          );
  },
    logout({commit}) {
        authService.logout();
      commit('logout');
    }
};

const mutations = {
    loginRequest(state, user) {
        state.status = {loggingIn: true};
        state.user = user;
    },
    loginSuccess(state, user) {
        state.status = {loggedIn: true};
        state.user = user;
    },
    loginFailure(state) {
        state.status = {};
        state.user = user;
    },
    logout(state) {
        state.status = {};
        state.user = null;
    }
}

export const authentication = {
    namespaced: true,
    state,
    actions,
    mutations
}
