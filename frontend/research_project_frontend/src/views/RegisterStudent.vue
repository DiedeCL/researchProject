<template>
<div class="page">
  <RegisterStudentForm/>
</div>

</template>

<script>
import RegisterStudentForm from "../components/student/RegistrationStudentForm.vue";
export default {
components:{
    RegisterStudentForm
}
}
</script>

<style>
.page {
    width: 100%;
    height: 93.8vh;
    background-image: url("../assets/background.jpg");
    text-align: center;
}
</style>