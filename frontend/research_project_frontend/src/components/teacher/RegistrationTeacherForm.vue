<template>
  <div>
    <h1 class="title">Registratie</h1>
    <div class="wrapper">
      <form method="post" class="form" id="form">
        <label for="name">Naam</label>
        <input name="name" type="text" class="input" placeholder="Naam" v-model="name" />
        <label for="firstName">Voornaam</label>
        <input
          name="firstName"
          type="text"
          class="input"
          placeholder="voornaam"
          v-model="firstName"
        />
        <label for="Email">Email</label>
        <input
          name="Email"
          class="input"
          type="email"
          placeholder="example@pxl.be"
          v-model="Email"
        />
        <label for="password">Wachtwoord
          <p class="passwordRules">Passwoord moet min. 8 karakters, een hoofdletter, een kleine letter en een cijfer bevatten</p>
        </label>
        <input
          name="password"
          title="Passwoord moet min. 8 karakters, een hoofdletter, een kleine letter en een cijfer bevatten"

          class="input"
          type="password"
          placeholder="Password"
          v-model="password"
        />
        <label for="password">Bevestig Wachtwoord</label>
        <input
          name="password"
          title="Passwoord moet min. 8 karakters, een hoofdletter, een kleine letter en een cijfer bevatten"
          class="input"
          type="password"
          placeholder="Password"
          v-model="confirmPassword"
        />
        <div id="alert"></div>
        <label for="teacherNumber">Personeel Nummer</label>
        <input
          name="teacherNumber"
          class="input"
          type="text"
          placeholder="Personeel number"
          v-model="teacherNumber"
        />

        <div class="buttons">
          <button class="button" type="button" @click="handleSubmit">Register</button>
        </div>
        
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterTeacherForm",
  data() {
    return {
      name: "",
      firstName: "",
      Email: "",
      password: "",
      confirmPassword: "",
      teacherNumber: ""
    };
  },
  methods: {
    handleSubmit() {
      let alert = document.getElementById("alert");
      alert.textContent = "";
      if (this.password == this.confirmPassword) {
        let url = "https://localhost:5001/api/Authentication/register";
        let teacherData = {
          Email: this.Email,
          Password: this.password,
          Name: this.name,
          FirstName: this.firstName,
          TeacherNumber: this.teacherNumber
        };
        fetch(url, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          },
          body: JSON.stringify(teacherData),
          mode: "cors"
        })
          .then(response => {
            if (response.status == 200 || response.status == 201) {
              this.$router.push("/login");
            } else {
              throw `error with status ${response.status}`;
            }
          })
          .catch(error => {
            this.$store.state.error = error;
          });
      } else {
        alert.textContent = "Wachtwoorden zijn niet gelijk";
      }
    }
  }
};
</script>

<style>
#alert {
  color: red;
}
h3 {
  margin-top: 8px;
  font-weight: bolder;
}
#streetLabel,
#townshipLabel {
  float: left;
}
#postalCodeLabel {
  margin-right: 58px;
}
#postalCodeLabel,
#numberLabel {
  float: right;
}
#numberLabel {
  margin-right: 40px;
}
#street {
  float: left;
  margin: 0;
  width: 80%;
}
#number {
  float: left;
  margin: 0;
  width: 18%;
  margin-left: 7px;
}
#township {
  float: left;
  margin: 0;
  width: 60%;
}
#postalNumber {
  float: left;
  margin: 0;
  width: 38%;
  margin-left: 6px;
}
.LabelAndInputWrapper {
  width: 50%;
  margin-left: 25%;
  clear: both;
}
.passwordRules {
  font-size: x-small;
  color: gray;
}

.title {
  margin: auto;
}
.form {
  margin: auto;
  width: 70%;
  margin-top: 30px;
  box-shadow: 0 0 8px 0 darkgray;
  border-radius: 10px;
  padding: 10px;
  font-family: arial;
  margin-bottom: 40px;
  padding: 50px 10%;
  background-color: white;
}
.buttons {
  margin-right: 25%;
  display: flex;
  justify-content: center;
  width: 50%;
  margin-left: 25%;
  margin-top: 8px;
}
.button {
  float: right;
  background-color: #58a518;
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
input {
  width: 50%;
  height: 40px;
  border-radius: 5px;
  padding: 10px;
  font-size: 18px;
  border: 1.5px solid gray;
  box-shadow: 0 0 1px black;
  transition: 0.1s ease;
  display: block;
  margin: 0 25%;
}

label {
  display: block;
  margin-top: 8px;
  font-weight: bold;
}

input[type="text"]:focus,
input[type="number"]:focus {
  box-shadow: 0 0 1px rgba(88, 165, 24, 0.8);
  transform: scale(1.01);
}
.alerts {
  color: red;
  font-size: x-large;
  font-weight: bold;
}
</style>