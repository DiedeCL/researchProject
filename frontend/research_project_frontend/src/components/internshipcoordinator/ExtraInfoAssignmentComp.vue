<template>
  <div class="form">
    <a @click="$router.push('/')" class="back">
      <img src="../../assets/backarrow.webp" width="4%" />
    </a>
    <p class="addTeacher" v-on:click="showList">Lector toevoegen</p>
    <p class="review" v-on:click="pushToReviews">Reviews</p>
    <assignteacherlist v-show="!isClosed" @close="closeList" v-bind:assignmentId="this.assignment.id" />
    <h1>Informatie opdracht</h1>
	<hr/>
    <h2>{{this.assignment.title}}</h2>
    <form v-if="this.assignment != ''">
      {{loadCheckboxes()}}
      <div class="column">
        <div class="box">
          <h2>Afstudeerrichting</h2>
          <hr />
          <p v-if="assignment.specialization == 0">Applicatie-ontwikkeling</p>
          <p v-else-if="assignment.specialization == 1">Systeem en Netwerk-beheer</p>
          <p v-else-if="assignment.specialization == 2">Software Management</p>
          <p v-else>AI en Robotics</p>
        </div>
        <div class="box">
          <h2>
            Omschrijving van de opdracht
            <span class="required">*</span>
          </h2>
          <hr />
          <p>{{assignment.description}}</p>
        </div>
        <div class="box">
          <h2>Omgeving</h2>
          <hr />
          <ul>
            <li v-if="omgeving.java == true">Java</li>
            <li v-if="omgeving.net == true">.NET (C#)</li>
            <li v-if="omgeving.web == true">Web (CSS, JavaScript, PHP, Angular,...)</li>
            <li v-if="omgeving.OS == true">Besturingssystemen (Linux, Windows,...)</li>
            <li
              v-if="omgeving.testing == true"
            >Software Testing, ITIL, Project Management, CRM, Information Management</li>
            <li v-if="extraOmgeving != ''">{{extraOmgeving}}</li>
          </ul>
        </div>
        <div class="box">
          <h2>
            Extra beschrijving omgeving
            <span class="required">*</span>
          </h2>
          <hr />
          <p>{{assignment.extraDescriptionEnvironments}}</p>
        </div>
        <div class="box">
          <h2>Randvoorwaarden</h2>
          <hr />
          <p v-if="assignment.conditions != ''">{{assignment.conditions}}</p>
          <p v-else>N.V.T.</p>
        </div>
        <div class="box">
          <h2>
            Onderzoeksthema
            <span class="required">*</span>
          </h2>
          <hr />
          <p>{{assignment.researchTheme}}</p>
        </div>
        <div class="box">
          <h2>
            Bedrijf
          </h2>
          <hr />
          <p>{{this.assignment.company.nameCompany}}</p>
        </div>
            <div class="box" style="padding-bottom: 40px">
               <h1>Locatie stage <span class="required">*</span></h1>
               <div class="row" style="padding-bottom: 45px">
                  <label style="width: 75%; float:left; margin-right: 3%; font-weight: bold">Straat</label>
                  <label style="width: 22%; float:left; font-weight: bold">Nr</label>
                  <hr>
                  <p style="width: 75%; float:left; margin-right: 3%">{{assignment.location.street}}</p>
                  <p style="width: 22%; float:left; margin-bottom: 10px">{{assignment.location.number}}</p>
               </div>
               <div class="row" style="margin-top: 20px">
                  <label style="width: 75%; float:left; margin-right: 3%; font-weight: bold">Gemeente</label>
                  <label style="width: 22%; float:left; font-weight: bold">Postcode</label>
                  <hr>
                  <p style="width: 75%; float:left; margin-right: 3%">{{assignment.location.township}}</p>
                  <p style="width: 22%; float:left; margin-bottom: 10px">{{assignment.location.postalNumber}}</p>
               </div>
            </div>
      </div>
      <div class="column">
        <div class="box">
          <h1>Overige</h1>
        </div>
        <div class="box">
          <h2>
            Aantal ondersteunende personeelsleden
            <span class="required">*</span>
          </h2>
          <hr />
          <p>{{assignment.amountOfSupportingEmployees}}</p>
        </div>
        <div class="box">
          <h2>Inleidende activiteiten / verwachtingen</h2>
          <hr />
          <ul>
            <li v-if="inleidendeActiviteit.sollicitatiegesprek == true">Sollicitatiegesprek</li>
            <li v-if="inleidendeActiviteit.cv == true">CV</li>
            <li
              v-if="inleidendeActiviteit.vergoeding == true"
            >Vergoeding / tegemoetkoming in verplaatskosten</li>
            <li v-if="extraInleidendeActiviteit != ''">{{extraInleidendeActiviteit}}</li>
          </ul>
        </div>
        <div class="box">
          <h2>
            Aantal gewenste stagiairs (max. 2 per stageproject)
            <span class="required">*</span>
          </h2>
          <hr />
          <p v-if="assignment.amountOfInterns == 1">1 student</p>
          <p v-if="assignment.amountOfInterns == 2">2 studenten</p>
        </div>
        <div class="box">
          <h2>Bedrijf is gecontacteerd door student(en) en wenst deze opdracht enkel aan deze student(en) aan te bieden</h2>
          <hr />
          <p
            v-if="assignment.specificStudentFirstAndLastName != ''"
          >{{assignment.specificStudentFirstAndLastName}}</p>
          <p v-else>N.V.T.</p>
        </div>
        <div class="box">
          <h2>Andere opmerkingen</h2>
          <hr />
          <p v-if="assignment.otherComments != ''">{{assignment.otherComments}}</p>
          <p v-else>N.V.T.</p>
        </div>
        <div class="box">
          <h2>
            Stageperiode
            <span class="required">*</span>
          </h2>
          <hr />
          <p v-if="assignment.internshipPeriod == 0">Semester 1 (oktober - januari)</p>
          <p v-else>Semester 2 (februari - juni)</p>
        </div>
      </div>
      <div class="column">
        <textarea
          name="admessage"
          v-model="admessage"
          rows="6"
          placeholder="Message"
          class="approvalMessage"
          style="margin-top: 20px"
        />
        <button>Goedkeuren</button>
        <button class="afkeurbutton">Afkeuren</button>
      </div>
    </form>
  </div>
</template>
<script>
import assignteacherlist from "./AssignTeacherList";
export default {
  name: "ExtraInfoAssignment",
  components: {
    assignteacherlist
  },
  data() {
    return {
      addmessage: "",
      isClosed: true,
      extraOmschrijving: "",
      apiUrl:
        "https://localhost:5001/api/Assignments/UpdateInternshipAssignment",
      omgeving: {
        java: false,
        NET: false,
        Web: false,
        OS: false,
        Testing: false,
        otherEnv: false
      },
      inleidendeActiviteit: {
        sollicitatiegesprek: false,
        cv: false,
        Vergoeding: false,
        other: false
      },
      extraInleidendeActiviteit: "",
      assignment: "",
      error: "",
      requestOptions: {
        method: "POST",
        body: JSON.stringify({ 
          Id: this.$route.query.id}),
        mode: "cors",
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
    fetch(
      "https://localhost:5001/api/Assignments/GetInternshipAssignmentById",
      this.requestOptions
    ).then(response =>
      response
        .json()
        .then(result => {
          if (!response.ok) {
            if (response.status == 400) {
              this.error = "Kan opdrachten niet opvragen van de servers";
            }
            return Promise.reject(this.error);
          }
          this.assignment = result;
        })
        .catch(error => {
          this.error = error;
        })
    );
  },
  methods: {
    closeList() {
      this.isClosed = true;
    },
    showList() {
      this.isClosed = false;
    },
    loadCheckboxes() {
      var list = this.assignment.environments;
      for (let i = 0; i < list.length; i++) {
        if (list[i].environmentName == "Java") {
          this.omgeving.java = true;
        } else if (list[i].environmentName == "NET") {
          this.omgeving.net = true;
        } else if (list[i].environmentName == "Web") {
          this.omgeving.web = true;
        } else if (list[i].environmentName == "OS") {
          this.omgeving.web = true;
        } else if (list[i].environmentName == "Testing") {
          this.omgeving.web = true;
        } else {
          this.omgeving.otherEnv = true;
          this.extraOmgeving = list[i].environmentName;
        }
      }
      list = this.assignment.introductionConditions;
      for (let i = 0; i < list.length; i++) {
        if (list[i].condition == "sollicitatiegesprek") {
          this.inleidendeActiviteit.sollicitatiegesprek = true;
        } else if (list[i].condition == "cv") {
          this.inleidendeActiviteit.cv = true;
        } else if (list[i].condition == "vergoeding") {
          this.inleidendeActiviteit.vergoeding = true;
        } else {
          this.inleidendeActiviteit.other = true;
          this.extraInleidendeActiviteit = list[i].condition;
        }
      }
      this.executed = 1;
    },
    pushToReviews() {
      this.$router.push('/extrainfo/reviews?id=' + this.assignment.id)
    }
  }
};
</script>

<style scoped>
.addTeacher {
  background-color: rgb(88, 165, 24);
  color: white;
  margin-right: 25px;
  margin-top: 20px;
  padding-top: 10px;
  padding-bottom: 10px;
  padding-left: 20px;
  padding-right: 20px;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  float: right;
}
.addTeacher:hover {
   cursor: pointer;
}
h1 {
  text-align: center;
}
h2 {
  text-align: center;
}
.box {
  background-color: #e0e0e0;
  padding: 15px;
  box-shadow: 0 0 4px #a0a0a0;
  margin-top: 15px;
  margin-bottom: 15px;
}
hr {
  margin-bottom: 10px;
}
ul {
  list-style-position: inside;
}
.back:hover {
  cursor: pointer;
}
.column {
  float: left;
  width: 50%;
  padding: 20px;
}
.approvalMessage {
  background-color: white;
}
.approvalMessage:hover {
  cursor: auto !important;
}
.form {
  overflow: auto;
  margin: 10px;
  width: 90%;
}
input:hover {
  cursor: not-allowed;
}
textarea:hover {
  cursor: not-allowed;
}
input:invalid {
  border: 2px dashed red;
}
.required {
  color: rgba(88, 165, 24, 0.8);
}
input[type="text"],
input[type="number"] {
  width: 100%;
  height: 40px;
  border-radius: 5px;
  padding: 10px;
  font-size: 18px;
  border: 1.5px solid gray;
  box-shadow: 0 0 1px black;
  transition: 0.1s ease;
  background-color: #e8e8e8;
}
input[type="text"]:focus,
input[type="number"]:focus {
  box-shadow: 0 0 1px rgba(88, 165, 24, 0.8);
  transform: scale(1.01);
}
textarea {
  border: 1.5px solid gray;
  padding: 5px;
  font-size: 15px;
  box-shadow: 0 0 2px 0 darkgray;
  transition: 0.1s ease;
  resize: none;
  width: 100%;
  border-radius: 5px;
  background-color: #e8e8e8;
}
textarea:focus {
  transform: scale(1.01);
  box-shadow: 0 0 1px green;
}
button:hover {
  cursor: pointer;
}
.companies {
  width: 100%;
  margin: 15px;
}
.header {
  width: 100%;
  border-bottom: 1px solid rgba(0, 0, 0, 0.2);
}
.company {
  border-bottom: 1px solid rgba(0, 0, 0, 0.2);
}
.nocompany {
  text-align: center;
  padding-top: 50px;
  font-weight: bolder;
  font-size: 1.5em;
}
button {
  background-color: rgb(88, 165, 24);
  color: white;
  padding-top: 10px;
  padding-bottom: 10px;
  padding-left: 30px;
  padding-right: 30px;
  border: none;
  border-radius: 7px;
  width: 45%;
  font-size: 18px;
  float: left;
  box-shadow: 0 0 5px rgba(88, 165, 24, 0.8);
}
.afkeurbutton {
  float: right;
  background-color: #a51818;
  box-shadow: 0 0 5px #a51818;
}
  .review {
    background-color: rgb(88, 165, 24);
    color: white;
    margin-right: 25px;
    margin-top: 20px;
    padding-top: 10px;
    padding-bottom: 10px;
    padding-left: 20px;
    padding-right: 20px;
    border: none;
    border-radius: 5px;
    font-size: 16px;
    float: right;
    width: 100px;
  }
  .review:hover {
    cursor: pointer;
  }
</style>