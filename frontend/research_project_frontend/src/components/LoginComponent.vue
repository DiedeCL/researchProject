<template>
        <div class="loginform">
            <img src="../assets/1314_logo_pxl_hogeschool.png" class="logo"/>
            <h1 class="title">Aanmelden</h1>
            <form class="form" @submit="handleSubmit">
                <input class="input" type="email" placeholder="Email" v-model="username"/>
                <input class="input" type="password" placeholder="Password" v-model="password"/>
                <div class="buttons">
                    <button class="button" type="submit">Aanmelden</button>
                    <button class="button" disabled>Registeren als bedrijf</button>
                </div>
                <div class="alerts" v-if="alert.message" :class="`alert ${alert.type}`">{{alert.message}}</div>
            </form>

        </div>
</template>

<script>
    import {mapState, mapActions} from 'vuex'
    export default {
        name: "LoginComponent",
        data() {
            return {
                username: "",
                password: "",
                submitted: false
            }
        },
        computed: {
            ...mapState('authentication', ['status']),
            ...mapState({
                alert: state => state.alert
            })
        },created () {
            // reset login status
            this.logout();
        },
        methods: {
            ...mapActions('authentication', ['login', 'logout']),
            handleSubmit() {
                this.submitted = true;
                const {username, password} = this;
                if(username && password) {
                    this.login({username, password});
                }
            },
            ...mapActions({
                clearAlert: 'alert/clear'
            })
        }
    }
</script>

<style scoped>
.loginform {
    background-color: white;
    border: 1px solid rgba(0, 0, 0, 0.15);
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
    width: 550px;
    margin: 150px 100px 0px 0px;
    height: 550px;
    text-align: center;
}
.logo {
    align-content: center;
    margin-top: 50px;
    margin-bottom: 70px;
    height: 70px;
    width: auto;
}
.input {
    width: 100%;
    padding: 8px;
    margin: 5px 0px;
    border: 1px solid black;
    border-radius: 5px;
}
.title {
    margin: auto
}
.form {
    text-align: center;
    width: 60%;
    margin: auto;
}
.buttons {
    justify-content: center;
    display: flex;
}
.button {
    background-color: #58A518;
    color: white;
    text-decoration: none;
    padding: 8px 27px;
    margin: 5px;
    border: none;
    border-radius: 5px;
}
.button:hover {
    cursor: pointer;
}
.button:disabled {
    background-color: #969696;
    color: #7e7e7e;
}

.alerts {
    color: red;
}
</style>