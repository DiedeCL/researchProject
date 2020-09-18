<template>
  <div class="page">
    <div class="component">
      <sidenavigation />
      <studentInfo />
    </div>
  </div>
</template>
<script>
import studentInfo from "../../components/internshipcoordinator/StudentInfo";
import sidenavigation from "../../components/internshipcoordinator/SideNavigation";
export default {
  name: "studentInfoPage",
  components: {
    studentInfo,
    sidenavigation
  }
};
</script>
<style scoped>
.page {
  height: 100%;
}
.component {
  display: flex;
}
</style>