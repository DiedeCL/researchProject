<template>
  <div>
    <nav class="navigationbar">
      <div class="banner">
        <img alt="logo" src="../assets/logo.png" class="logo" />
        <p class="stage">Stage</p>
      </div>
      <div v-if="isLoggedIn" class="dropdown">
        <p class="user">{{user.email}}</p>
        <div class="dropdown-content">
            <router-link id="changepassword" class="dropdown-component" to="/wachtwoordWijzigen">wachtwoord wijzigen</router-link>
            <p id="logout" class="dropdown-component" v-on:click="handleLogout">afmelden</p>
        </div>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "topnavigation",
};
</script>

<style scoped>
.navigationbar {
  width: 100%;
  background-color: black;
  color: white;
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.banner {
  display: flex;
  align-items: center;
}
.stage {
  display: inline-block;
  font-size: 1.3em;
  margin-left: 7px;
  font-weight: bold;
}
.logo {
  width: 50px;
  height: auto;
  margin-left: 15px;
  display: inline-block;
}

.dropdown {
  position: relative;
  display: inline-block;
  height: auto;
}
.user {
  padding: 19px;
}

.dropdown-content {
  padding: 10px;
  text-align: center;
  display: none;
  position: absolute;
  margin-top: -10px;
  background-color: black;
  box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
  z-index: 1;
  border-radius: 10px;

}

#changepassword {
  text-decoration: none; 
}

.dropdown-component {
  padding-left: 15px;
  padding-right: 15px;
  padding-top: 10px;
  padding-bottom: 5px;
  color: white;
  font-size: 16px;
}

.dropdown-component:hover {
  color: rgb(88, 165, 24);
  cursor: pointer;
}

.dropdown:hover .dropdown-content {display: block;}
</style>