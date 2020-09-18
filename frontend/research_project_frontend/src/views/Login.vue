<template>
    <div class="page">
        <img class="cover" src="../assets/cover.png" alt="cover image"/>
        <LoginComponent class="login"></LoginComponent>
    </div>
</template>

<script>
    import LoginComponent from "../components/LoginComponent";
    export default {
        name: 'Login',
        components: {LoginComponent},
    }
</script>

<style scoped>
.page {
    height: 93.8vh;
    width: 100%;
    background-image: url("../assets/background.jpg");
    display: flex;
}
.cover {
    height: 100%;
    width: auto;
}
.login {
    width: 550px;
    margin: 150px 100px;
    height: 550px;
}
.background {
    width: 100%;
    height: 93.8vh;
}

</style>