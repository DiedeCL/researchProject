import Vue from 'vue';
import Vuex from 'vuex';

import {authentication} from './authenticationModule'
import {alert} from './alertModule'

Vue.use(Vuex);

export default new Vuex.Store({
  state: {
  },
  mutations: {
  },
  actions: {
  },
  modules: {
    authentication,
    alert
  }
})
