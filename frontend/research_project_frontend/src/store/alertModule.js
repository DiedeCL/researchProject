const state = {
    type: null,
    message: null
};

const actions = {
    error({commit}, message) {
        commit('error', message);
    }
};

const mutations = {
    error(state, message) {
        state.type = 'alert-danger';
        state.message = message;
    }
};

export const alert = {
    namespaced: true,
    state,
    actions,
    mutations
};