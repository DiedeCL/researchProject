<template>
  <table class="assignment">
      <col width="20%" />
      <col width="45%" />
      <col width="10%" />
      <col width="15%" />
    <tr class="tr">
      <td>{{assignment.specialization}}</td>
      <td v-if="assignment.description.length<80">{{assignment.description}}</td>
      <td v-else>{{assignment.description.substring(0,100)+ "..."}}</td>
      <td><statusIndicator v-bind:status="assignment.status.toLowerCase()" /></td>
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

</style>