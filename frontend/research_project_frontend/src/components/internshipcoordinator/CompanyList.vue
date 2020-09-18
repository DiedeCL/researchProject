<template>
  <div class>
    <table class="header">
      <col width="10%" />
      <col width="14%" />
      <col width="14%" />
      <col width="16%" />
      <col width="16%" />
      <col width="16%" />
      <col width="16%" />
      <tr class="tr">
        <th>Naam</th>
        <th># medewerkers</th>
        <th># IT-medewerkers</th>
        <th>Address</th>
        <th>Contactpersoon</th>
        <th>Tel. nummer</th>
        <th>E-mail</th>
      </tr>
    </table>
    <div v-if="companyList.length > 0">
      <div v-for="(company) in companyList" v-bind:key="company.id">
        <company class="company" v-bind:company="company" />
      </div>
    </div>
    <div v-else-if="error.length > 0">{{error}}</div>
    <div v-else class="nocompany">
      <p>Geen bedrijven om weer te geven.</p>
    </div>
  </div>
</template>

<script>
import company from "./Company";

export default {
  name: "companylist",
  components: {
    company
  },
  data() {
    return {
      companyList: [],
      error: "",
      requestOptions: {
        method: "GET",
        mode: "cors",
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
  created() {
    fetch(
      "https://localhost:5001/api/company/getallcompanies",
      this.requestOptions
    ).then(response => {
      response
        .json()
        .then(companies => {
          if (!response.ok) {
            if (response.status == 400) {
              this.error = "Kan bedrijven niet opvragen van de servers";
            }
            return Promise.reject(this.error);
          }
          companies.forEach(company => {
            console.log(company);
            this.companyList.push(company);
          });
        })
        .catch(error => {
          this.error = error;
        });
    });
  }
};
</script>

<style scoped>
.companies {
  width: 100%;
  margin: 15px;
}
.header {
  width: 100%;
  border-bottom: 1px solid rgba(0, 0, 0, 0.2);
}
.company {
  border-bottom: 1px solid rgba(0, 0, 0, 0.2);
}
.nocompany {
  text-align: center;
  padding-top: 50px;
  font-weight: bolder;
  font-size: 1.5em;
}
.routerLink {
  text-decoration: none;
  color: black;
}
.company:hover {
  background-color: rgba(0, 0, 0, 0.1);
}
</style>