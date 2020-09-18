<template>
  <nav class="sidenavigation">
    <div class="sidenavigationitems">
        <router-link to="/nieuweopdracht" class="sidenavigationlink newassignment">Nieuwe Opdracht</router-link>
        <router-link to="/overzicht" class="sidenavigationlink active">Opdrachten</router-link>
    </div>
  </nav>
</template>

<script>
export default {
  name: "sidenavigationstagecoordinator",
  data() {
    return {};
  }
};
</script>

<style scoped>
.sidenavigation {
  background-color: rgba(245, 245, 245, 1);
  width: 15vw;
  height: 93.8vh;
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
.active {
    background-color: rgba(88, 165, 24, 0.8);
    color: white;
}
.newassignment {
  text-decoration: none;
  font-weight: bold;
  font-size: 1.4em;
  width: 90%;
  padding: 10px;
  margin: auto;
  margin-bottom: 12px;
  margin-top: 12px;
  display: block;
  color: white;
  background-color: rgba(88, 165, 24, 1);
  border-radius: 5px;
}
</style>