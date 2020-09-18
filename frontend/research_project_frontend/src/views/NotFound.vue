<template>
  <div class="page">
      <img class="cover" src="../assets/cover.png" alt="cover image"/>
    <div class="error">
      <h1 class="code">404</h1>
      <h2 class="message">De gevraagde pagina werd niet gevonden</h2>
      <a class="button" href="javascript:history.go(-1)">Klik hier om terug te gaan</a>
    </div>
  </div>
</template>

<script>
export default {
  name: "NotFound"
};
</script>
<style scoped>
.page {
    height: 93.8vh;
    width: 100%;
    background-image: url("../assets/background.jpg");
    text-align: center;
}
.cover {
    float: left;
    height: 100%;
    width: auto;
}
.error {
    float: right;
    background-color: white;
    border: 1px solid rgba(0, 0, 0, 0.15);
    box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.25);
    width: 550px;
    margin: 150px 100px 0px 0px;
    height: 550px;
}
.code {
    margin: 50px 0px 0px 0px;
    font-size: 7.5em;
    font-weight: normal;
}
.message {
    margin: 200px 0px 20px 0px;
    font-size: 1.5em;
    font-weight: normal;

}
.button {
    background-color: #58A518;
    color: white;
    text-decoration: none;
    padding: 8px 15px;
}
</style>