<template>
  <div id="app">
    <topnavigation />
    <router-view />
  </div>
</template>

<script>
import topnavigation from "./components/TopNavigationComponent";

export default {
  name: "app",
  components: {
    topnavigation
  }
};
</script>

<style>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}
body {
  line-height: 1.4;
  font-family: Arial, Helvetica, sans-serif;
  height: 100vh;
  width: 100%;
}
</style>
