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
          placeholder="example@student.pxl.be"
          v-model="Email"
        />
        <label for="password">
          Wachtwoord
          <p class="passwordRules">Passwoord moet min. 8 karakters, een hoofdletter, een kleine letter en een cijfer bevatten</p>
        </label>
        <input
          name="password"
          class="input"
          title="Passwoord moet min. 8 karakters, een hoofdletter, een kleine letter en een cijfer bevatten"
          type="password"
          placeholder="Password"
          v-model="password"
        />
        <label for="password">Bevestig Wachtwoord</label>
        <input
          name="password"
          class="input"
          type="password"
          title="Passwoord moet min. 8 karakters, een hoofdletter, een kleine letter en een cijfer bevatten"
          placeholder="Password"
          v-model="confirmPassword"
        />
        <div id="alert"></div>
        <label for="studentNumber">Studenten Nummer</label>
        <input
          name="studentNumber"
          class="input"
          type="text"
          placeholder="Studenten number"
          v-model="studentNumber"
        />
        <div class="LabelAndInputWrapper">
          <label for="street" id="streetLabel">Straat</label>
          <label for="number" id="numberLabel">Nr</label>
        </div>
        <div class="LabelAndInputWrapper">
          <input
            name="street"
            type="text"
            id="street"
            class="input"
            placeholder="Straat"
            v-model="street"
          />
          <input
            name="number"
            type="text"
            id="number"
            class="input"
            placeholder="Nr"
            v-model="houseNumber"
          />
        </div>

        <div class="LabelAndInputWrapper">
          <label for="township" id="townshipLabel">Gemeente</label>
          <label for="postalNumber" id="postalCodeLabel">Postcode</label>
        </div>
        <div class="LabelAndInputWrapper">
          <input
            name="township"
            type="text"
            id="township"
            class="input"
            placeholder="Gemeente"
            v-model="township"
          />
          <input
            name="postalNumber"
            type="text"
            id="postalNumber"
            class="input"
            placeholder="Postcode"
            v-model="postalNumber"
          />
        </div>
        <div style="clear:both;">
          <h3>Afstudeer richting</h3>
          <select class="specialization" v-model="specialization">
            <option disabled value>Kies een afstudeer richting</option>
            <option value="ApplicationDevelopment">Applicatie-ontwikkeling</option>
            <option value="SystemAndNetwork">Systeem en Netwerk-beheer</option>
            <option value="SoftwareManagement">Software Management</option>
            <option value="AiAndRobotics">Ai en Robotics</option>
          </select>
        </div>
        <div class="buttons">
          <button class="button" type="button" @click="handleSubmit">Register</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterStudentForm",
  data() {
    return {
      specialization: "",
      name: "",
      firstName: "",
      Email: "",
      password: "",
      confirmPassword: "",
      studentNumber: "",
      street: "",
      houseNumber: "",
      township: "",
      postalNumber: ""
    };
  },
  mounted() {
    let code = this.$route.params.code;
    const requestOptions = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "application/json"
      },
      mode: "cors",
      body: JSON.stringify({ code })
    };
    fetch(`https://localhost:5001/api/authentication/ValidateRegistrationUrl`, requestOptions)
            .then((response) => {
                if(response.status == 400) {
                    this.$router.push({ name: 'NotFound'})
                }
            })
  },
  methods: {
    handleSubmit() {
      if (this.password == this.confirmPassword) {
        let url = "https://localhost:5001/api/Authentication/register";
        let studentData = {
          Email: this.Email,
          Password: this.password,
          Specialization: this.specialization,
          Name: this.name,
          FirstName: this.firstName,
          StudentNumber: this.studentNumber,
          Street: this.street,
          HouseNumber: this.houseNumber,
          Township: this.township,
          PostalNumber: this.postalNumber
        };
        fetch(url, {
          method: "POST",
          headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
          },
          body: JSON.stringify(studentData),
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
        document.getElementById("alert").textContent =
          "Wachtwoorden zijn niet gelijk";
      }
    }
  }
};
</script>

<style >
.specialization {
  margin:5px;
  padding: 7px;
  border: 1px solid rgba(88, 165, 24, 1);
  border-radius: 5px;
}
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