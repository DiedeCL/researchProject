<template>
    <nav class="sidenavigation">
        <div class="sidenavigationitems">
            <router-link to="/" class="sidenavigationlink" exact-active-class="active">Opdrachten</router-link>
        </div>
    </nav>
</template>

<script>
    export default {
        name: "SideNavigationTeacher"
    }
</script>

<style scoped>
    .sidenavigation {
        background-color: rgba(245, 245, 245, 1);
        width: 15vw;
        min-height: 93.4vh;
        text-align: center;
    }
    .sidenavigationlink {
        text-decoration: none;
        font-weight: bold;
        font-size: 1.4em;
        width: 100%;
        padding: 15px;
        display: block;
        color: black;
    }
    .sidenavigationlink:hover {
        background-color: rgba(0, 0, 0, 0.1);
    }
    .active {
        background-color: rgba(88, 165, 24, 0.8);
        color: white;
    }
    .active:hover {
        background-color: rgba(88, 165, 24, 0.9);
    }
</style>