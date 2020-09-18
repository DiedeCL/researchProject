<template>
    <div class="form">
        <a @click="$router.push('/')" class="back"><img src="../../assets/backarrow.webp" width="4%"></a>
        <h1>Informatie opdracht</h1>
        <div v-if="assignment.length != 0">
            <form @submit="assignmentUpdate" v-if="this.assignment != ''">
                    {{loadCheckboxes()}}
                <div class="feedback" v-if="assignment.reviewMessage">
                    <h1>Feedback stagecoördinator</h1>
                    <p>{{assignment.reviewMessage}}</p>
                    <hr/>
                </div>

                <div class="column">       

                    <h2>titel <span class="required">*</span></h2>
                    <input type="text" id="title" name="title" rows="6" cols="50" style="resize: none; width: 100%; border-radius: 5px" placeholder="titel" v-model="assignment.title" required />

                    <br>
                    <br>
                    <h3>Afstudeerrichting</h3>
                    <p>Selecteer hier de afstudeerrichting waarvoor de stageopdracht bestemd is:</p>

                    <input type="radio" id="aon" name="richting" value="0" v-model="assignment.specialization">
                    <label for="aon"> Applicatie-ontwikkeling</label>
                    <br>
                    <input type="radio" id="snb" name="richting" value="1" v-model="assignment.specialization">
                    <label for="snb"> Systeem en Netwerk-beheer</label>
                    <br>
                    <input type="radio" id="swm" name="richting" value="2" v-model="assignment.specialization">
                    <label for="swm"> Software Management</label>
                    <br>
                    <input type="radio" id="ai" name="richting" value="3" v-model="assignment.specialization">
                    <label for="ai"> AI en Robotics</label>

                    <br>
                    <br>
                    <h2>Omschrijving van de opdracht <span class="required">*</span></h2>
                    <p>Beschrijf hier duidelijk en zo gedetailleerd mogelijk wat de stageopdracht inhoudelijk zal omvatten:</p>
                    <textarea id="omschrijving" rows="6" cols="50" style="resize: none; width: 100%; border-radius: 5px" placeholder="Beschrijf opdracht" v-model="assignment.description" required />
                    <br>
                    <br>
                    <h2>Omgeving</h2>
                    <p>in welke IT-omgeving dient de stageopdracht uitgewerkt te worden:</p>
                    <input type="checkbox" name="java" value="java" v-model="omgeving.java">
                    <label for="java"> Java</label>
                    <br>
                    <input type="checkbox" name="NET" value="NET" v-model="omgeving.net">
                    <label for="NET"> .NET (C#)</label>
                    <br>
                    <input type="checkbox" name="Web" value="Web" v-model="omgeving.web">
                    <label for="Web"> Web (CSS, JavaScript, PHP, Angular,...)</label>
                    <br>
                    <input type="checkbox" name="OS" value="OS" v-model="omgeving.OS">
                    <label for="OS"> Besturingssystemen (Linux, Windows,...)</label>
                    <br>
                    <input type="checkbox" name="Testing" value="Testing" v-model="omgeving.testing">
                    <label for="Testing"> Software Testing, ITIL, Project Management, CRM, Information Management</label>
                    <br>
                    <input type="checkbox" name="andereOmgeving" v-model="omgeving.otherEnv">
                    <label for="Andere"> Andere:</label>
                    <br>
                    <input :disabled="!omgeving.otherEnv" type="text" id="Andere" name="omgeving" v-model="extraOmgeving">

                    <br>
                    <br>
                    <h2>Extra beschrijving omgeving <span class="required">*</span></h2>
                    <textarea id="extraOmschrijving" name="extraOmschrijving" rows="6" cols="50" style="resize: none; width: 100%; border-radius: 5px" placeholder="Extra omschrijvijng omgeving" v-model="assignment.extraDescriptionEnvironments" required />

                    <br>
                    <br>
                    <h2>Randvoorwaarden</h2>
                    <p>Zijn er specifieke eisen waaraan moet worden voldaan voor het uitvoeren van de stageopdracht vb bereidheid tot communicatie in het Engels, beschikken over een auto, bereikbaar met openbaar vervoer,...</p>
                    <input type="text" id="randvoorwaarden" name="randvoorwaarden" placeholder="Randvoorwaarden" v-model="assignment.conditions">

                    <br>
                    <br>
                    <h2>Onderzoeksthema <span class="required">*</span></h2>
                    <p>Onderzoek dat leidt tot het vergaren van diepgaande kennis voor het oplossen van een praktijkprobleem. Het thema kan zich situeren in het kader of in het verlengde van de stageopdracht ofwel een specifieke onderzoeksopdracht aangebracht door het bedrijf (zie begeleidende brief voor enkele voorbeelden)</p>
                    <input type="text" id="thema" name="thema" placeholder="Onderzoeksthema" v-model="assignment.researchTheme" required>
                </div>

                <div class="column">
                    <h1>Locatie stage <span class="required">*</span></h1>

                    <div class="row">
                        <label style="width: 75%; float:left; margin-right: 3%">Straat</label>
                        <label style="width: 22%; float:left">Nr</label>
                        <input type="text" id="straat" name="straat" placeholder="Straat" style="width: 75%; float:left; margin-right: 3%" v-model="assignment.location.street" required>
                        <input type="number" name="nummer" placeholder="Nr" style="width: 22%; float:left; margin-bottom: 10px" v-model="assignment.location.number" required>
                    </div>

                    <div class="row">
                        <label style="width: 75%; float:left; margin-right: 3%">Gemeente</label>
                        <label style="width: 22%; float:left">Postcode</label>
                        <input type="text" id="gemeente" name="gemeente" placeholder="Gemeente" style="width: 75%; float:left; margin-right: 3%" v-model="assignment.location.township" required>
                        <input type="number" id="postcode" name="postcode" placeholder="Postcode" style="width: 22%; float:left; margin-bottom: 30px" v-model="assignment.location.postalNumber" required>
                    </div>

                    <h1>Overige</h1>
                    <br>
                    <h2>Aantal ondersteunende personeelsleden <span class="required">*</span></h2>
                    <p>Deze mensen kunnen de student technisch begeleiden bij het uitwerken van de stageopdracht</p>
                    <input type="number" name="aantalPersoneel" id="aantalPersoneel" placeholder="Aantal ondersteunende personeelsleden" v-model="assignment.amountOfSupportingEmployees" required>

                    <br>
                    <br>

                    <h2>Inleidende activiteiten / verwachtingen</h2>
                    <input type="checkbox" id="sollicitatiegesprek" name="inleidendeActiviteit" value="sollicitatiegesprek" v-model="inleidendeActiviteit.sollicitatiegesprek">
                    <label for="sollicitatiegesprek"> Sollicitatiegesprek</label>
                    <br>
                    <input type="checkbox" id="cv" name="inleidendeActiviteit" value="cv" v-model="inleidendeActiviteit.cv">
                    <label for="cv"> CV</label>
                    <br>
                    <input type="checkbox" id="Vergoeding" name="inleidendeActiviteit" value="Vergoeding" v-model="inleidendeActiviteit.vergoeding">
                    <label for="Vergoeding"> Vergoeding / tegemoetkoming in verplaatskosten</label>
                    <br>
                    <input type="checkbox" id="andereActiviteit" name="inleidendeActiviteit" v-model="inleidendeActiviteit.other">
                    <label for="andereInleidendeActiviteit"> Andere:</label>
                    <br>
                    <input :disabled="!inleidendeActiviteit.other" type="text" id="Andere" name="inleidendeActiviteit" v-model="extraInleidendeActiviteit">

                    <br>
                    <br>
                    <h2>Aantal gewenste stagiairs (max. 2 per stageproject) <span class="required">*</span></h2>
                    <input type="radio" id="1" name="aantalStagiairs" value="1" v-model="assignment.amountOfInterns" required>
                    <label for="1"> 1 student</label>
                    <br>
                    <input type="radio" id="2" name="aantalStagiairs" value="2" v-model="assignment.amountOfInterns">
                    <label for="2"> 2 studenten</label>
                    <br>

                    <br>
                    <br>
                    <h2>Bedrijf is gecontacteerd door student(en) en wenst deze opdracht enkel aan deze student(en) aan te bieden</h2>
                    <input type="text" name="specifiekeStudent" id="specifiekeStudent" placeholder="Naam + Voornaam student" v-model="assignment.specificStudentFirstAndLastName">

                    <br>
                    <br>
                    <h2>Andere opmerkingen</h2>
                    <input type="text" name="opmerkingen" id="opmerkingen" placeholder="Andere opmerkingen" v-model="assignment.otherComments">

                    <br>
                    <br>
                    <h2>Stageperiode <span class="required">*</span></h2>
                    <input type="radio" id="sem1" name="periode" required="true" value="0" v-model="assignment.internshipPeriod">
                    <label for="sem1"> Semester 1 (oktober - januari)</label>
                    <br>
                    <input type="radio" id="sem2" name="periode" value="1" v-model="assignment.internshipPeriod">
                    <label for="sem2"> Semester 2 (februari - juni)</label>
                    <br>

                    <center>
                        <button type="submit">Aanvraag wijzigingen</button>
                    </center>
                </div>
            </form>
        </div>
        <div v-else-if="error.length > 0">
            <h2 class="error">{{error}}</h2></div>
        <div v-else>
            <h2 class="error">Er is een fout opgetreden bij het opvragen van de opdracht.</h2></div>
    </div>
</template>

<script>
export default {
    name: "ExtraInfoAssignment",
    components: {

    },
    data() {
    return {
      extraOmschrijving: '',
      extraOmgeving: '',
      OmgevingString: '',
      InleidendeActiviteitString: '',
      apiUrl: "https://localhost:5001/api/Assignments/UpdateInternshipAssignment",
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
	extraInleidendeActiviteit: '',
      assignment: '',

      error: "",
      requestOptions: {
        method: 'POST',
        body: JSON.stringify({Id: this.$route.query.id}),
        mode: 'cors',
        headers: {
          'Authorization' : `Bearer ${JSON.parse(localStorage.getItem('user')).token}`,
          'Acces-Control-Allow-Origin': '*',
          'Accept' : 'application/json',
			'Content-Type': 'application/json',
        },
      }
  };
  },
  mounted() {
    fetch("https://localhost:5001/api/Assignments/GetInternshipAssignmentById", this.requestOptions).then(response =>
      response.json().then(result => {
        if (!response.ok) {
          if (response.status == 400) {
            this.error = "Kan opdrachten niet opvragen van de servers";
          }
          return Promise.reject(this.error);
        }
			console.log(result)
			this.assignment = result;
			
        })
        .catch(error => {
          this.error = error;
        })
    );
	
},
methods: {
	loadCheckboxes() {
		var list = this.assignment.environments;
		for(let i = 0; i < list.length; i++) {
			if(list[i].environmentName == "Java") {
				this.omgeving.java = true;
			} else if(list[i].environmentName == "NET") {
				this.omgeving.net = true;
			}else if(list[i].environmentName == "Web") {
				this.omgeving.web = true;
			}else if(list[i].environmentName == "OS") {
				this.omgeving.OS = true;
			}else if(list[i].environmentName == "Testing") {
				this.omgeving.testing = true;
			} else {
				this.omgeving.otherEnv = true;
				this.extraOmgeving = list[i].environmentName;
			}
		}

		list = this.assignment.introductionConditions;
		for(let i = 0; i < list.length; i++) {
			if(list[i].condition == "sollicitatiegesprek") {
				this.inleidendeActiviteit.sollicitatiegesprek = true;
			} else if(list[i].condition == "cv") {
				this.inleidendeActiviteit.cv = true;
			}else if(list[i].condition == "vergoeding") {
				this.inleidendeActiviteit.vergoeding = true;
			} else {
				this.inleidendeActiviteit.other = true;
				this.extraInleidendeActiviteit = list[i].condition;
			}
		}
	
	},
	assignmentUpdate(e) {
	e.preventDefault();

	this.InleidendeActiviteitString = '';
	if(this.inleidendeActiviteit.sollicitatiegesprek == true) {
		this.InleidendeActiviteitString = "sollicitatiegesprek,";
	}
	if(this.inleidendeActiviteit.cv == true) {
		this.InleidendeActiviteitString += "cv,";
	}
	if(this.inleidendeActiviteit.vergoeding == true) {
		this.InleidendeActiviteitString += "vergoeding,";
	}
	if(this.inleidendeActiviteit.other == true) {
		this.InleidendeActiviteitString += this.extraInleidendeActiviteit + ",";
	}
	this.InleidendeActiviteitString = this.InleidendeActiviteitString.slice(0, -1);
	
	this.OmgevingString = '';
	
	if(this.omgeving.java == true) {
		this.OmgevingString += "Java,";
	}if(this.omgeving.net == true) {
		this.OmgevingString += "NET,";
	}if(this.omgeving.web == true) {
		this.OmgevingString += "Web,";
	}if(this.omgeving.OS == true) {
		this.OmgevingString += "OS,";
	}if(this.omgeving.testing == true) {
		this.OmgevingString += "Testing,";
	}if(this.omgeving.otherEnv == true) {
		this.OmgevingString += this.extraOmgeving + ",";
	}
	
	this.OmgevingString = this.OmgevingString.slice(0, -1);


	delete this.assignment.environments;
	delete this.assignment.introductionConditions;
	
	this.assignment.environments = this.OmgevingString;
	this.assignment.introductionConditions = this.InleidendeActiviteitString;
	this.assignment['ExtraDescr'] = "test";
	this.assignment['street'] = this.assignment.location.street;
	this.assignment['postalNumber'] = this.assignment.location.postalNumber;
	this.assignment['township'] = this.assignment.location.township;
	this.assignment['number'] = parseInt(this.assignment.location.number);

				fetch(this.apiUrl,
			{
				method: "POST",
                        
				headers: {
					'Accept' : 'application/json',
					'Content-Type': 'application/json',
					'Authorization' : `Bearer ${JSON.parse(localStorage.getItem('user')).token}`,
				},
				body: JSON.stringify(this.assignment),
				mode : 'cors'
				})
				.then((response) => {
					if (response.status == 200 || response.status == 201) {
						alert("Assignment aangepast");
						this.$router.push('/');
						return response.json();
						
					} else {
						throw `error with status ${response.status}`;
					}
				})
				.catch((error) => {
					this.$store.state.error = error;
				});

}
	
},


};
</script>

<style scoped>
h1 {
  text-align: center;
}
.error {
	background-color: lightgray;
	text-align: center;
	margin: 20%;
	border-radius: 5px;
}

.back:hover {
	cursor: pointer;
}
.column {
	float:left;
	width: 50%;
	padding: 20px;
}
.feedback {
    margin: 10px;
    text-align: center;
}
.form {
margin: 10px;
width: 90%;
}
input:invalid {
  border: 2px dashed red;
}
.required {
	color: rgba(88, 165, 24, 0.8);
}
input[type=text], input[type=number] {
	width: 100%;
	height: 40px;
	border-radius: 5px;
	padding: 10px;
	font-size: 18px;
	border: 1.5px solid gray;
	box-shadow: 0 0 1px black;
	transition: 0.1s ease;
}

input[type=text]:focus, input[type=number]:focus{

	box-shadow: 0 0 1px rgba(88, 165, 24, 0.8);
	transform: scale(1.01);
}

textarea {
	border: 1.5px solid gray;
	padding: 5px;
	font-size: 15px;
	box-shadow: 0 0 2px 0 darkgray;
		transition: 0.1s ease;
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
	width: 70%;
	font-size: 18px;
	margin-top: 20px;
	box-shadow: 0 0 5px rgba(88, 165, 24, 0.8);
}
</style>