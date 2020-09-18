<template>
  <div class="overview">
    <div v-if="account.type == 'internshipcoordinator'" class="component">
      <sidenavigationinternshipcoordinator class="sidenavigation"/>
      <assignmentlistinternshipcoordinator class="assignmentlist"/>
    </div>
    <div v-if="account.type == 'companypromoter'" class="component">
      <sidenavigationcompany class="sidenavigation" />
      <assignmentlistcompany class="assignmentlist" v-bind:account="account"/>
    </div>
  </div>
</template>

<script>
import sidenavigationinternshipcoordinator from "../components/internshipcoordinator/SideNavigation";
import assignmentlistinternshipcoordinator from "../components/internshipcoordinator/AssignmentList";

import sidenavigationcompany from "../components/company/SideNavigation";
import assignmentlistcompany from "../components/company/AssignmentList";

import sidenavigationstudent from "../components/student/SideNavigation";
import assignmentliststudent from "../components/student/AssignmentList";

import sidenavigationteacher from "../components/teacher/SideNavigationTeacher"
import assignmentlistteacher from "../components/teacher/AssignmentListTeacher"

let user = JSON.parse(localStorage.getItem('user'));

export default {
  name: "overview",
  components: {
    sidenavigationinternshipcoordinator,
    assignmentlistinternshipcoordinator,
    sidenavigationcompany,
    assignmentlistcompany
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
    }
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
