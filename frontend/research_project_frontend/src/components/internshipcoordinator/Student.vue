<template>
    <div class="component" v-on:click="extraInfo(student.id)">
        <table class="student">
            <col width="25%" />
            <col width="25%" />
            <col width="25%" />
            <col width="25%" />
            <tr class="tr">
            <td>{{student.name + " " + student.firstName}}</td>
            <td>{{student.studentNumber}}</td>
            <td>{{student.specialization}}</td>
            <td>{{student.email}}</td>
            </tr>
        </table>
  </div>
</template>

<script>
export default {
  name: "student",
  props: ["student"],
  methods: {
		extraInfo(id) {
      console.log(id);
			this.$router.push('/studentinfo?id=' + id);
		}
  }
};
</script>

<style scoped>
.student {
  text-align: center;
  width: 100%;
  border-collapse: collapse;
  padding-top: 8px;
  padding-bottom: 8px;
}
.component:hover {
  cursor: pointer;
	background-color: #f5f5f5;
}
</style>