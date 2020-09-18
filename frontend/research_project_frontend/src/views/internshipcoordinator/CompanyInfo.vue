<template>
  <div class="page">
    <div class="component">
      <sidenavigation />
      <companyInfo />
    </div>
  </div>
</template>
<script>
import companyInfo from "../../components/internshipcoordinator/CompanyInfo";
import sidenavigation from "../../components/internshipcoordinator/SideNavigation";
export default {
  name: "studentInfoPage",
  components: {
    companyInfo,
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