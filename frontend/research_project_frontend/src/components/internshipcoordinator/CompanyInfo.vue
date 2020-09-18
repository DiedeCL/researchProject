<template>
  <div class="app">
    <div class="container">
      <router-link to="/bedrijven">
        <img src="../../assets/left-arrow.svg" class="backImage" />
      </router-link>
      <div class="title">
        <h1>{{company.nameCompany}}</h1>
      </div>
      <div class="info">
        <div class="general section">
          <h2>Algemene Informatie</h2>
          <p>Aantal IT personeel: {{company.amountOfITPersonnel}}</p>
          <p>Aantal personeel: {{company.amountOfPersonnel}}</p>
        </div>
        <div class="address section">
          <h2>Address</h2>
          <p>{{company.addressCompany.street}}, {{company.addressCompany.number}}</p>
          <p>{{company.addressCompany.township}}, {{company.addressCompany.postalNumber}}</p>
        </div>
        <div class="contact section">
          <h2>Contact persoon</h2>
          <p>Voornaam: {{company.contactCompany.name}}</p>
          <p>Achternaam: {{company.contactCompany.lastName}}</p>
          <p>Job titel: {{company.contactCompany.jobTitle}}</p>
          <h3 class="contactinfo">Contact gegevens</h3>
          <p>
            Email:
            <a
              class="mail"
              v-bind:href="`mailto:${company.contactCompany.email}`"
            >{{company.contactCompany.email}}</a>
          </p>
          <p>GSM nummer: {{formatPhoneNumbers(company.contactCompany.phoneNumber)}}</p>
        </div>
        <div class="promoter section">
          <h2>Promoter</h2>
          <p>Voornaam: {{company.promoter.name}}</p>
          <p>Achternaam: {{company.promoter.lastName}}</p>
          <p>Job titel: {{company.promoter.jobTitle}}</p>
          <h3 class="contactinfo">Contact gegevens</h3>
          <p>
            Email:
            <a
              class="mail"
              v-bind:href="`mailto:${company.promoter.email}`"
            >{{company.promoter.email}}</a>
          </p>
          <p>GSM nummer: {{formatPhoneNumbers(company.promoter.phoneNumber)}}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "bedrijfInfo",
  data() {
    return {
      company: {},
      number: 0,
      requestOptions: {
        method: "POST",
        mode: "cors",
        body: JSON.stringify({
          CompanyId: this.$route.query.id
        }),
        headers: {
          Authorization: `Bearer ${
            JSON.parse(localStorage.getItem("user")).token
          }`,
          "Acces-Control-Allow-Origin": "*",
          Accept: "application/json",
          "Content-Type": "application/json"
        }
      }
    };
  },
  methods: {
    formatPhoneNumbers(number) {
      if (number == "undefined" || number == null)
        return "invalid phone number";
      let numbersplit = number.match(/.{1,2}/g);
      return (
        numbersplit[0] +
        numbersplit[1] +
        " " +
        numbersplit[2] +
        " " +
        numbersplit[3] +
        " " +
        numbersplit[4]
      );
    }
  },
  mounted() {
    fetch(
      "https://localhost:5001/api/company/GetCompanyById",
      this.requestOptions
    ).then(response => {
      response
        .json()
        .then(company => {
          if (!response.ok) {
            if (response.status == 400) {
              this.error = "Kan het bedrijf niet opvragen van de servers";
            }
            return Promise.reject(this.error);
          }
          this.company = company;
          console.log(this.company)
        })
        .catch(error => {
          this.error = error;
        });
    });
  }
};
</script>

<style scoped>
.app {
  height: 93.8vh;
  width: 100%;
  background-image: url("../../assets/background.jpg");
  display: flex;
}
.container {
  margin: auto;
  width: 60%;
  margin-top: 30px;
  box-shadow: 0 0 8px 0 rgb(206, 206, 206);
  border-radius: 5px;
  padding: 10px;
  font-family: arial;
  margin-bottom: 40px;
  padding-bottom: 50px;
  padding-top: 50px;
  background-color: white;
}
.title {
  text-align: center;
}
.backImage {
  margin: 25px;
  height: 50px;
  width: auto;
}
.info {
  justify-content: center;
  display: flex;
  flex-wrap: wrap;
}
.section {
  margin-top: 50px;
  line-height: 1.7;
  flex-grow: 2;
  text-align: center;
  flex-basis: 50%;
}
.contactinfo {
  margin-top: 15px;
}
.mail {
  color: rgb(88, 165, 24);
}
</style>