<template>
  <div class="app">
    <div class="container">
      <router-link to="/studenten">
        <img src="../../assets/left-arrow.svg" class="backImage" />
      </router-link>
      <div class="name">
        <h1>{{this.student.firstName}} {{this.student.name}}</h1>
        <br>
        <br>
        <h3>Email:</h3>
        <a class="mail" v-bind:href="`mailto:${student.email}`">{{this.student.email}}</a>
        <br>
        <br>
        <h3>Student Nummer:</h3>
        <p>{{this.student.studentNumber}}</p>
        <br>
        <br>
        <h3>Afstudeer richting:</h3>
        <p>{{this.student.specialization}}</p>
        <p></p>
      </div>
      <div class="info">
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "studentInfo",
  data() {
    return {
      student: {},
      requestOptions: {
        method: "POST",
        mode: "cors",
        body: JSON.stringify({ 
          Id: this.$route.query.id
        }),
        headers: {
          Authorization: `Bearer ${
            JSON.parse(localStorage.getItem("user")).token
          }`,
          "Acces-Control-Allow-Origin": "*",
          Accept: "application/json",
          "Content-Type": "application/json"
        }
      }
    };
  },
  mounted() {
    console.log(this.$route.query.id)
    fetch(
      "https://localhost:5001/api/student/GetStudentById",
      this.requestOptions
    ).then(response => {
      response
        .json()
        .then(student => {
          if (!response.ok) {
            if (response.status == 400) {
              this.error = "Kan het student niet opvragen van de servers";
            }
            return Promise.reject(this.error);
          }
          this.student = student;
        })
        .catch(error => {
          this.error = error;
        });
    });
  }
};
</script>

<style scoped>
.app {
  height: 93.8vh;
  width: 100%;
  background-image: url("../../assets/background.jpg");
  display: flex;
}
.container {
  margin: auto;
  width: 40%;
  margin-top: 10%;
  box-shadow: 0 0 8px 0 rgb(206, 206, 206);
  border-radius: 5px;
  padding: 10px;
  font-family: arial;
  padding-bottom: 160px;
  padding-top: 20px;
  background-color: white;
}
.name {
  text-align: center;
}
.backImage {
  margin: 25px;
  height: 50px;
  width: auto;
}
.info {
  justify-content: center;
  display: flex;
  flex-wrap: wrap;
}
.section {
  margin-top: 50px;
  line-height: 1.7;
  flex-grow: 2;
  text-align: center;
  flex-basis: 50%;
}
.contactinfo {
    margin-top: 15px;
}
.mail {
  color: rgb(88, 165, 24);
}
</style>