<template>
  <table class="assignment">
    <col width="20%" />
    <col width="10%" />
    <col width="10%" />
    <col width="30%" />
    <col width="10%" />
    <col width="10%" />
    <col width="10%" />
    <tr class="tr">
      <td>{{assignment.specialization}}</td>
      <td>{{assignment.companyName}}</td>
      <td v-if="assignment.enviroments.length <20">{{assignment.enviroments}}</td>
      <td v-else>{{assignment.enviroments.substring(0,20)+"..."}}</td>
      <td>{{assignment.title}}</td>
      <td><statusIndicator v-bind:status="assignment.status.toLowerCase()"/></td>
      <td><statusIndicator v-bind:status="assignment.teacherStatus.toLowerCase()" /></td>
      <td>{{assignment.dateCreated.split('T')[0]}}</td>
    </tr>
  </table>
</template>

<script>
import statusIndicator from "../AssignmentStatusComponent"

export default {
  name: "assignment",
  props: ["assignment"],
  components: {
    statusIndicator
  }
};
</script>

<style scoped>
.assignment {
  text-align: center;
  width: 100%;
  border-collapse: collapse;
}
td{
  max-width: 100px;

}

</style>