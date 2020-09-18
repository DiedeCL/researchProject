<template>
<div class="form">
<div class="content">
    <h1 class="body">Aanvraag Stageopdracht</h1>   
	<br><br>
	<h2 style="text-align: center">Stageopdacht</h2>
	<div class="topicInfo">
	<p style="text-align: center"><a href="https://docs.google.com/document/d/1MiIpI7U51rwLPKmZY2Cvq7tSqqtEloRp8lvVE06DRj4/edit?usp=sharing">info onderzoekstopic (klik hier) <br>
</a> </p>
</div>
<br>

<form @submit="assignmentSend">
<h2>Afstudeerrichting <span class="required">*</span></h2>
<p>Selecteer hier de afstudeerrichting waarvoor de stageopdracht bestemd is:</p>

<input type="radio" id="aon" name="richting" value="applicationdevelopment" v-model="afstudeerrichting" required>
<label for="aon"> Applicatie-ontwikkeling</label><br>
<input type="radio" id="snb" name="richting" value="systemandnetwork" v-model="afstudeerrichting">
<label for="snb"> Systeem en Netwerk-beheer</label><br>
<input type="radio" id="swm" name="richting" value="softwaremanagement" v-model="afstudeerrichting">
<label for="swm"> Software Management</label> <br>
<input type="radio" id="ai" name="richting" value="aienrobotics" v-model="afstudeerrichting">
<label for="ai"> AI en Robotics</label> 

<br><br>
<h2>Omschrijving van de opdracht <span class="required">*</span></h2>
<p>Beschrijf hier duidelijk en zo gedetailleerd mogelijk wat de stageopdracht inhoudelijk zal omvatten:</p>
<textarea id="omschrijving" rows="6" cols="50"  style="resize: none; width: 100%; border-radius: 5px" placeholder="Beschrijf opdracht" v-model="beschrijving" required/>

<br><br>
<h2>Omgeving</h2>
<p>in welke IT-omgeving dient de stageopdracht uitgewerkt te worden:</p>
<input type="checkbox" name="java" value="java" v-model="omgeving">
<label for="java"> Java</label><br>
<input type="checkbox" name="NET" value="NET" v-model="omgeving">
<label for="NET"> .NET (C#)</label><br>
<input type="checkbox" name="Web" value="Web" v-model="omgeving">
<label for="Web"> Web (CSS, JavaScript, PHP, Angular,...)</label> <br>
<input type="checkbox" name="OS" value="OS" v-model="omgeving">
<label for="OS"> Besturingssystemen (Linux, Windows,...)</label>  <br>
<input type="checkbox" name="Testing" value="Testing" v-model="omgeving">
<label for="Testing"> Software Testing, ITIL, Project Management, CRM, Information Management</label>  <br>
<input type="checkbox" name="andereOmgeving" v-model="andereOmgeving">
<label for="Andere"> Andere:</label>  <br>
<input :disabled="!andereOmgeving" type="text" id="Andere" name="omgeving" v-model="extraOmgeving">

<br><br>
<h2>Exta beschrijving omgeving <span class="required">*</span></h2>
<input type="text" id="extraOmschrijving" name="extraOmschrijving" placeholder="Extra omschrijvijng omgeving" v-model="extraOmschrijvingOmgeving" required>

<br><br>
<h2>Randvoorwaarden</h2>
<p>Zijn er specifieke eisen waaraan moet worden voldaan voor het uitvoeren van de stageopdracht vb bereidheid tot communicatie in het Engels, beschikken over een auto, bereikbaar met openbaar vervoer,...</p>
<input type="text" id="randvoorwaarden" name="randvoorwaarden" placeholder="Randvoorwaarden" v-model="randvoorwaarden">

<br><br>
<h2>Onderzoeksthema <span class="required">*</span></h2>
<p>Onderzoek dat leidt tot het vergaren van diepgaande kennis voor het oplossen van een praktijkprobleem. Het thema kan zich situeren in het kader of in het verlengde van de stageopdracht ofwel een specifieke onderzoeksopdracht aangebracht door het bedrijf (zie begeleidende brief voor enkele voorbeelden)</p>
<input type="text" id="thema" name="thema" placeholder="Onderzoeksthema" v-model="onderzoeksthema" required>


<br><br><br><br>
<h1>Locatie stage <span class="required">*</span></h1>

<div>
<div class="row">
<label style="width: 75%; float:left; margin-right: 3%">Straat</label>
<label style="width: 22%; float:left">Nr</label>
<input type="text" id="straat" name="straat" placeholder="Straat" style="width: 75%; float:left; margin-right: 3%" v-model="adresStraat" required>
<input type="number" name="nummer" placeholder="Nr" style="width: 22%; float:left; margin-bottom: 10px" v-model="adresNr" required>
</div>

<div class="row" >
<label style="width: 75%; float:left; margin-right: 3%">Germeente</label>
<label style="width: 22%; float:left">Postcode</label>
<input type="text" id="gemeente" name="gemeente" placeholder="Gemeente" style="width: 75%; float:left; margin-right: 3%" v-model="adresGemeente" required>
<input type="number" id="postcode" name="postcode" placeholder="Postcode" style="width: 22%; float:left; margin-bottom: 30px" v-model="adresPostcode" required>
</div>
</div>


<h1>Overige</h1>
<br>
<h2>Aantal ondersteunende personeelsleden <span class="required">*</span></h2>
<p>Deze mensen kunnen de student technisch begeleiden bij het uitwerken van de stageopdracht</p>
<input type="number" name="aantalPersoneel" id="aantalPersoneel" placeholder="Aantal ondersteunende personeelsleden" v-model="aantalPersoneel" required>

<br><br>
<h2>Inleidende activiteiten / verwachtingen</h2>
<input type="checkbox" id="sollicitatiegesprek" name="inleidendeActiviteit" value="sollicitatiegesprek" v-model="inleidendeActiviteit">
<label for="sollicitatiegesprek"> Sollicitatiegesprek</label><br>
<input type="checkbox" id="cv" name="inleidendeActiviteit" value="cv" v-model="inleidendeActiviteit">
<label for="cv"> CV</label><br>
<input type="checkbox" id="Vergoeding" name="inleidendeActiviteit" value="Vergoeding" v-model="inleidendeActiviteit"> 
<label for="Vergoeding"> Vergoeding / tegemoetkoming in verplaatskosten</label> <br>
<input type="checkbox" id="andereActiviteit" name="inleidendeActiviteit" v-model="andereInleidendeActiviteit">
<label for="andereInleidendeActiviteit"> Andere:</label>  <br>
<input :disabled="!andereInleidendeActiviteit" type="text" id="Andere" name="inleidendeActiviteit" v-model="extraInleidendeActiviteit">

<br><br>
<h2>Aantal gewenste stagiairs (max. 2 per stageproject) <span class="required">*</span></h2>
<input type="radio" id="1" name="aantalStagiairs" value="1" v-model="aantalStagiairs" required>
<label for="1"> 1 student</label><br>
<input type="radio" id="2" name="aantalStagiairs" value="2" v-model="aantalStagiairs">
<label for="2"> 2 studenten</label><br>

<br><br>
<h2>Bedrijf is gecontacteerd door student(en) en wenst deze opdracht enkel aan deze student(en) aan te bieden</h2>
<input type="text" name="specifiekeStudent" id="specifiekeStudent" placeholder="Naam + Voornaam student" v-model="specifiekeStudent">

<br><br>
<h2>Andere opmerkingen</h2>
<input type="text" name="opmerkingen" id="opmerkingen" placeholder="Andere opmerkingen" v-model="opmerkingen">

<br><br>
<h2>Stageperiode <span class="required">*</span></h2>
<input type="radio" id="sem1" name="stageperiode" value="semester1" v-model="stageperiode" required>
<label for="sem1"> Semester 1 (oktober - januari)</label><br>
<input type="radio" id="sem2" name="stageperiode" value="semester2" v-model="stageperiode">
<label for="sem2"> Semester 2 (februari - juni)</label><br>

<center><button type="submit">Aanvraag indienen</button></center>
</form>

</div>
</div>
</template>

<script>
export default {
	name: "assignmentform",
data (){
	return {
		afstudeerrichting: '',
		andereOmgeving: false,
		beschrijving: '',
		omgevingTotaal: '',
		omgeving: [],
		extraOmschrijvingOmgeving: '',
		randvoorwaarden: '',
		onderzoeksthema: '',
		adres: {},
		adresStraat: '',
		adresNr: '',
		adresGemeente: '',
		adresPostcode: '',
		aantalPersoneel: '',
		inleidendeActiviteit: [],
		inleidendeActiviteitTotaal: '',
		andereInleidendeActiviteit: false,
		extraInleidendeActiviteit: '',
		aantalStagiairs: '',
		specifiekeStudent: '',
		opmerkingen: '',
		stageperiode: '',
		extraOmgeving: '',
		apiUrl: "https://localhost:5001/api/Assignments/AddInternshipAssignments"
		}
},

methods: {
	assignmentSend(e) {
		e.preventDefault();
		if(this.extraInleidendeActiviteit !== '') {
			this.inleidendeActiviteit.push(this.extraInleidendeActiviteit)
		} 
		if(this.extraOmgeving !== ''){
			this.omgeving.push(this.extraOmgeving);
			
		}
		
		for(var j = 0; j < this.inleidendeActiviteit.length; j++) {
			if(j == 0) {
				this.inleidendeActiviteitTotaal = this.inleidendeActiviteit[j]
			} else {
				this.inleidendeActiviteitTotaal = this.inleidendeActiviteitTotaal + "," + this.inleidendeActiviteit[j]
			}
		}
		
		
		for(var i = 0; i < this.omgeving.length; i++) {
			if(i ==0) {
				this.omgevingTotaal = this.omgeving[i]
			}else {
				this.omgevingTotaal = this.omgevingTotaal + "," + this.omgeving[i]
			}
			
		}
		
		let assignmentJson =  {
				"specialization": this.afstudeerrichting,
				"description": this.beschrijving,
				"Enviroments": this.omgevingTotaal,
				"Township": this.adresGemeente,
				"Postalnumber": this.adresPostcode,
				"Number": parseInt(this.adresNr),
				"Street": this.adresStraat,
				"ExtraDesc": this.extraOmschrijvingOmgeving,
				"Conditions": this.randvoorwaarden,
				"researchTheme": this.onderzoeksthema,
				"amountOfSupportingEmployees": parseInt(this.aantalPersoneel),
				"IntroductionCondition": this.inleidendeActiviteitTotaal,
				"amountOfInterns": parseInt(this.aantalStagiairs),
				"specificStudentFirstAndLastName": this.specifiekeStudent,
				"otherComments": this.opmerkingen,
				"internshipPeriod": this.stageperiode,
				"CompanyId": parseInt(`${JSON.parse(localStorage.getItem('user')).companyId}`)
			};


			fetch(this.apiUrl,
			{
				method: "POST",
                        
				headers: {
					'Accept' : 'application/json',
					'Content-Type': 'application/json',
					'Authorization' : `Bearer ${JSON.parse(localStorage.getItem('user')).token}`,
				},
				body: JSON.stringify(assignmentJson),
				mode : 'cors'
				})
				.then((response) => {
					if (response.status == 200 || response.status == 201) {
						alert("Assignment verstuurd");
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

}
};
</script>

<style scoped>
.form {
	margin: auto;
	width: 70%;
	margin-top: 30px;
	box-shadow: 0 0 8px 0 darkgray;
	border-radius: 10px;
	padding: 10px;
	font-family: arial;
	margin-bottom: 40px;
	padding-bottom: 50px;
	padding-top: 50px;
	background-color: white;
}

.test {
	background-color: red;
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

h1 {
 text-align: center
}

h2 {
	font-weight: normal
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

.topicInfo {
	background-color: #f0f0f0;
	border: 1px solid #e3e3e3;
	margin-top: 1%;
	box-shadow: 0 0 5px #e3e3e3;
	padding: 8px;
	border-radius: 5px;
}

button:hover {
	cursor: pointer;
}

.content {
	margin-left: 25%;
	margin-right: 25%;
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

.content 
{
width: 50%
}
</style>