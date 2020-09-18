<template>
  <div class="overview">
    <div v-if="account.type == 'internshipcoordinator'" class="component">
      <sidenavigationinternshipcoordinator />
      <extrainfoassignmentInternshipCoordinator />
    </div>
    <div v-if="account.type == 'company'" class="component">
      <sidenavigationcompany />
      <extrainfoassignmentCompany />
    </div>
    <div v-if="account.type == 'student'" class="component">
      <sidenavigationstudent />
      <extrainfoassignmentStudent />
    </div>
    <div v-if="account.type == 'teacher'" class="component">
      <sidenavigationteacher />
      <extrainfoassignmentTeacher />
    </div>
  </div>
</template>

<script>
import extrainfoassignmentInternshipCoordinator from "../components/internshipcoordinator/ExtraInfoAssignmentComp";
import extrainfoassignmentCompany from "../components/company/ExtraInfoAssignmentComp";
import extrainfoassignmentStudent from "../components/student/ExtraInfoAssignmentComp";
import extrainfoassignmentTeacher from "../components/teacher/ExtraInfoAssignmentComp";

import sidenavigationcompany from "../components/company/SideNavigation";
import sidenavigationinternshipcoordinator from "../components/internshipcoordinator/SideNavigation";
import sidenavigationstudent from "../components/student/SideNavigation";
import sidenavigationteacher from "../components/teacher/SideNavigationTeacher";

let user = JSON.parse(localStorage.getItem("user"));

export default {
  name: "extrainfo",
  components: {
    extrainfoassignmentInternshipCoordinator,
    extrainfoassignmentCompany,
    sidenavigationcompany,
    sidenavigationinternshipcoordinator,
    sidenavigationstudent,
    extrainfoassignmentStudent,
    sidenavigationteacher,
    extrainfoassignmentTeacher
  },
  data() {
    return {
      authenticated: false,
      account: {
        name: user.email,
        type: user.role[0].toLowerCase(),
        username: user.name,
        password: "password"
      }
    };
  }
};
</script>

<style scoped>
.overview {
  height: 100%;
}
.component {
  display: flex;
}
</style>
