<template>
  <nav class="sidenavigation">
    <div class="sidenavigationitems">
        <a href="/overzicht" class="sidenavigationlink active">Opdrachten</a>
    </div>
  </nav>
</template>

<script>
export default {
  name: "sidenavigationstagecoordinator"
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
</style>