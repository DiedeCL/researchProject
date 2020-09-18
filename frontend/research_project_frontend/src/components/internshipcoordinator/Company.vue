<template>
  <div class="component" v-on:click="extraInfo(company.companyId)">
    <table class="company">
      <col width="10%" />
      <col width="14%" />
      <col width="14%" />
      <col width="16%" />
      <col width="16%" />
      <col width="16%" />
      <col width="16%" />
      <tr class="tr">
        <td>{{company.nameCompany}}</td>
        <td>{{company.amountOfPersonnel}}</td>
        <td>{{company.amountOfITPersonnel}}</td>
        <td>{{company.addressCompany.street}} {{company.addressCompany.number}}, {{company.addressCompany.postalNumber}} {{company.addressCompany.township}}</td>
        <td>{{company.contactCompany.name}} {{company.contactCompany.lastName}}</td>
        <td>{{company.contactCompany.phoneNumber}}</td>
        <td>{{company.contactCompany.email}}</td>
      </tr>
    </table>
  </div>
</template>

<script>
export default {
  name: "company",
  props: ["company"],
  methods: {
    extraInfo(id) {
      console.log(id);
      this.$router.push("/bedrijfinfo?id=" + id);
    }
  }
};
</script>

<style scoped>
.company {
  text-align: center;
  width: 100%;
  border-collapse: collapse;
}
.component {
  padding-top: 8px;
  padding-bottom: 8px;
}
.component:hover {
  cursor: pointer;
}
</style>