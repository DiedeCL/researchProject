<template>
<div class="page">
  <RegisterTeacherForm/>
</div>

</template>

<script>
import RegisterTeacherForm from "../components/teacher/RegistrationTeacherForm.vue";
export default {
components:{
    RegisterTeacherForm
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