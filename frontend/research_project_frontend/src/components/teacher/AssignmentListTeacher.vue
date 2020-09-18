<template>
    <div class="assignments">
        <div class="sortAndFilterOptions">
            <select id="selectSpecialization"  v-model="selectedFilterSpecialization">
                <option selected value> Afstudeerrichting </option>
                <option value="Ai And Robotics">Ai en Robotics</option>
                <option value="Application Development">Applicatie-ontwikkeling</option>
                <option value="Software Management">Software Management</option>
                <option value="System And Network">Systeem en Netwerk-beheer</option>
            </select>
            <select id="selectFilterEnvironment" v-model="selectedFilterEnvironments">
            </select>
            <select id="selectFilterStatus" v-model="selectedFilterStatus">
                <option selected value>Status</option>
                <option value="Opdracht verzonden">Opdracht Verzonden</option>
                <option value="Afgekeurd">Afgekeurd</option>
                <option value="Behandeling">In behandeling</option>
                <option value="Goedgekeurd">Goedgekeurd</option>
            </select>
            <select id="selectFilterStatusLector" v-model="selectedFilterStatusLector">
                <option selected value>Lector Status</option>
                <option value="Geen lector toegevoegd">Geen lector</option>
                <option value="Verschillende reviews">Lector toegewezen</option>
                <option value="Afgekeurd">Afgekeurd</option>
                <option value="Behandeling">In behandeling</option>
                <option value="Goedgekeurd">Goedgekeurd</option>
            </select>
            <div>
                <input id="searchOnCompany" class="searchBox" type="text" placeholder="Zoeken Bedrijf" title="Zoeken op bedrijf" v-model="searchOnCompany">
            </div>
            <div>
                <input id="searchOnTitle" class="searchBox" type="text" placeholder="Zoeken Titel" title="Zoeken op titel" v-model="search">
            </div>
        </div>
        <table class="header">
            <col width="20%" />
            <col width="10%" />
            <col width="10%" />
            <col width="30%" />
            <col width="10%" />
            <col width="10%" />
            <tr class="tr">
                <th v-on:click="OnSpecializationClick">Afstudeerrichting
                    <div id="arrowSpecializationBoth" class="arrowsDiv">
                        <i id="arrowSpecializationUp" class="up arrowsPosition"></i>
                        <i id="arrowSpecializationDown" class="down "></i>
                    </div>
                </th>
                <th v-on:click="OnCompanyClick">Bedrijf
                    <div id="arrowCompanyBoth" class="arrowsDiv">
                        <i id="arrowCompanyUp" class="up arrowsPosition"></i>
                        <i id="arrowCompanyDown" class="down"></i>
                    </div>
                </th>
                <th v-on:click="OnEnvironmentClick">Omgeving
                    <div id="arrowEnvironmentBoth" class="arrowsDiv">
                        <i id="arrowEnvironmentUp" class="up arrowsPosition"></i>
                        <i id="arrowEnvironmentDown" class="down"></i>
                    </div>
                </th>
                <th v-on:click="OnTitleClick">Titel
                    <div id="arrowTitleBoth" class="arrowsDiv">
                        <i id="arrowTitleUp" class="up arrowsPosition"></i>
                        <i id="arrowTitleDown" class="down"></i>
                    </div>
                </th>
                <th v-on:click="OnStatusLectorClick">Lector Status
                    <div id="arrowStatusLectorBoth" class="arrowsDiv">
                        <i id="arrowStatusLectorUp" class="up arrowsPosition"></i>
                        <i id="arrowStatusLectorDown" class="down"></i>
                    </div></th>
                <th v-on:click="OnDateClick">Datum aangemaakt
                    <div id="arrowDateBoth" class="arrowsDiv">
                        <i id="arrowDateUp" class="up arrowsPosition"></i>
                        <i id="arrowDateDown" class="down"></i>
                    </div>
                </th>
            </tr>
        </table>
        <div v-if="assignmentList.length > 0">
            <assignment class="assignment" v-for="(item, index) in computed_items" v-bind:key="item.id" v-bind:index="index" v-bind:assignment="item"/>
        </div>
        <div v-else-if="error.length > 0">{{error}}</div>
        <div v-else class="noassignments">
            <p >Geen opdrachten om weer te geven.</p>
        </div>
    </div>
</template>

<script>
    import assignment from "./Assignment";
    export default {
        name: "AssignmentListTeacher",
        components: {
            assignment
        },
        data() {
            return {
                assignmentList: [],
                error: "",
                requestOptions: {
                    method: 'POST',
                    mode: 'cors',
                    body: JSON.stringify({
                        "email": `${JSON.parse(localStorage.getItem('user')).email}`
                    }),
                    headers: {
                        'Authorization' : `Bearer ${JSON.parse(localStorage.getItem('user')).token}`,
                        'Acces-Control-Allow-Origin': '*',
                        'Accept' : 'application/json',
                        'Content-Type': 'application/json',
                    },
                },
                counterSpecialization: 0,
                counterCompany: 0,
                counterEnvironment: 0,
                counterTitle: 0,
                counterStatus: 0,
                counterStatusLector: 0,
                counterDate: 0,
                search: "",
                selectedFilterSpecialization: "",
                selectedFilterStatus : "",
                selectedFilterStatusLector : "",
                selectedFilterEnvironments : "",
                searchOnCompany: ""
            };
        },
        mounted() {
            fetch("https://localhost:5001/api/assignments/GetAllAssignedAssingments", this.requestOptions).then(response =>
                response.json().then(assignments => {
                    if (!response.ok) {
                        if (response.status == 400) {
                            this.error = "Kan opdrachten niet opvragen van de servers";
                        }
                        return Promise.reject(this.error);
                    }
                    assignments.forEach(assignment => {
                        this.assignmentList.push(assignment)
                    });
                })
                    .catch(error => {
                        this.error = error;
                    })
            );
            this.FillFilterEnvironments();
        },
        computed : {
            computed_items: function () {
                this.FillFilterEnvironments();

                let filterSpecialization = this.selectedFilterSpecialization;
                let search = this.search;
                let filterStatus = this.selectedFilterStatus;
                let filterEnvironment = this.selectedFilterEnvironments;
                let searchOnCompany = this.searchOnCompany;
                let filterStatusLector = this.selectedFilterStatusLector;

                let list = this.assignmentList;
                if(filterSpecialization === "Afstudeerrichting" && search === ""  && filterStatus === "Status" && filterEnvironment === "Omgeving" && searchOnCompany === "Bedrijven" && filterStatusLector === "Lector Status"){
                    return this.assignmentList;
                }else{
                    if(filterSpecialization !==  "Afstudeerrichting" && filterSpecialization !== ""){
                        list = list.filter((assignment =>{
                            return assignment.specialization.match(filterSpecialization);
                        }));
                    }
                    if(search !== ""){
                        list = list.filter((assignment) => {
                            return assignment.title.toLowerCase().match(search.toLowerCase());
                        });
                    }
                    if(filterStatus !== "Status" && filterStatus !== ""){
                        list = list.filter((assignment) =>{
                            return assignment.status.match(filterStatus);
                        })
                    }
                    if(filterEnvironment !== "Omgeving" && filterEnvironment !== ""){
                        list = list.filter((assignment) =>{
                            return assignment.enviroments.toLowerCase().match(filterEnvironment);
                        })
                    }

                    if(searchOnCompany !== ""){
                        list = list.filter((assignment) => {
                            return assignment.companyName.toLowerCase().match(searchOnCompany.toLowerCase());
                        });
                    }

                    if(filterStatusLector !== "Lector Status" && filterStatusLector !== ""){
                        list = list.filter((assignment) => {
                            return assignment.teacherStatus.match(filterStatusLector);
                        })
                    }
                }
                return list;
            }
        },
        methods :{
            OnSpecializationClick(){
                this.counterSpecialization++;

                let arrowSpecializationUp = document.getElementById("arrowSpecializationUp");
                let arrowSpecializationDown = document.getElementById("arrowSpecializationDown");

                if(this.counterCompany !== 0){
                    this.ChangeColorsArrows();
                    this.counterCompany = 0;
                }
                if(this.counterEnvironment !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterTitle !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterStatus !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterStatusLector !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterDate !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }


                if(this.counterSpecialization === 1){
                    this.assignmentList.sort((a,b) => a.specialization.toLowerCase() > b.specialization.toLowerCase() ? 1 : -1);
                    arrowSpecializationUp.style.borderColor = "#000000";
                    arrowSpecializationDown.style.borderColor = "#D3D3D3"
                }else if(this.counterSpecialization === 2){
                    this.assignmentList.sort((a,b) => a.specialization.toLowerCase() > b.specialization.toLowerCase() ? -1 : 1);
                    arrowSpecializationUp.style.borderColor = "#D3D3D3";
                    arrowSpecializationDown.style.borderColor = "#000000";
                }else if(this.counterSpecialization === 3){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowSpecializationDown.style.borderColor = "#D3D3D3";
                    this.counterSpecialization = 0;
                }
            },
            OnCompanyClick(){
                this.counterCompany++;
                let arrowCompanyUp = document.getElementById("arrowCompanyUp");
                let arrowCompanyDown = document.getElementById("arrowCompanyDown");


                if(this.counterSpecialization !== 0){
                    this.ChangeColorsArrows();
                    this.counterSpecialization = 0;
                }
                if(this.counterEnvironment !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterTitle !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterStatus !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterStatusLector !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterDate !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }

                if(this.counterCompany === 1){
                    this.assignmentList.sort((a,b) => a.companyName > b.companyName ? 1 : -1);
                    arrowCompanyUp.style.borderColor = "#000000";
                    arrowCompanyDown.style.borderColor = "#D3D3D3"
                }else if(this.counterCompany === 2){
                    this.assignmentList.sort((a,b) => a.companyName > b.companyName  ? -1 : 1);
                    arrowCompanyUp.style.borderColor = "#D3D3D3";
                    arrowCompanyDown.style.borderColor = "#000000";
                }else if(this.counterCompany === 3){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowCompanyDown.style.borderColor = "#D3D3D3";
                    this.counterCompany = 0;
                }
            },
            OnEnvironmentClick(){
                this.counterEnvironment++;

                let arrowEnvironmentUp = document.getElementById("arrowEnvironmentUp");
                let arrowEnvironmentDown = document.getElementById("arrowEnvironmentDown");


                if(this.counterCompany !== 0){
                    this.ChangeColorsArrows();
                    this.counterCompany = 0;
                }
                if(this.counterSpecialization !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterTitle !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterStatus !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterStatusLector !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterDate !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }

                if(this.counterEnvironment === 1){
                    this.assignmentList.sort((a,b) => a.enviroments.toLowerCase() > b.enviroments.toLowerCase() ? 1 : -1);
                    arrowEnvironmentUp.style.borderColor = "#000000";
                    arrowEnvironmentDown.style.borderColor = "#D3D3D3"
                }else if(this.counterEnvironment === 2){
                    this.assignmentList.sort((a,b) => a.enviroments.toLowerCase() > b.enviroments.toLowerCase() ? -1 : 1);
                    arrowEnvironmentUp.style.borderColor = "#D3D3D3";
                    arrowEnvironmentDown.style.borderColor = "#000000";
                }else if(this.counterEnvironment === 3){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowEnvironmentDown.style.borderColor = "#D3D3D3";
                    this.counterEnvironment = 0;
                }
            },
            OnTitleClick(){
                this.counterTitle++;

                let arrowTitleUp = document.getElementById("arrowTitleUp");
                let arrowTitleDown = document.getElementById("arrowTitleDown");


                if(this.counterCompany !== 0){
                    this.ChangeColorsArrows();
                    this.counterCompany = 0;
                }
                if(this.counterEnvironment !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterSpecialization !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterStatus !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterStatusLector !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterDate !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }

                if(this.counterTitle === 1){
                    this.assignmentList.sort((a,b) => a.description.toLowerCase() > b.description.toLowerCase() ? 1 : -1);
                    arrowTitleUp.style.borderColor = "#000000";
                    arrowTitleDown.style.borderColor = "#D3D3D3"
                }else if(this.counterTitle === 2){
                    this.assignmentList.sort((a,b) => a.description.toLowerCase() > b.description.toLowerCase() ? -1 : 1);
                    arrowTitleUp.style.borderColor = "#D3D3D3";
                    arrowTitleDown.style.borderColor = "#000000";
                }else if(this.counterTitle === 3){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowTitleDown.style.borderColor = "#D3D3D3";
                    this.counterTitle = 0;
                }
            },
            OnStatusClick(){
                this.counterStatus++;

                let arrowStatusUp = document.getElementById("arrowStatusUp");
                let arrowStatusDown = document.getElementById("arrowStatusDown");


                if(this.counterCompany !== 0){
                    this.ChangeColorsArrows();
                    this.counterCompany = 0;
                }
                if(this.counterEnvironment !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterTitle !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterSpecialization !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterStatusLector !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterDate !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }

                if(this.counterStatus === 1){
                    this.assignmentList.sort((a,b) => a.status.toLowerCase() > b.status.toLowerCase() ? -1 : 1);
                    arrowStatusUp.style.borderColor = "#000000";
                    arrowStatusDown.style.borderColor = "#D3D3D3"
                }else if(this.counterStatus === 2){
                    this.assignmentList.sort((a,b) => a.status.toLowerCase() > b.status.toLowerCase() ? 1 : -1);
                    arrowStatusUp.style.borderColor = "#D3D3D3";
                    arrowStatusDown.style.borderColor = "#000000";
                }else if(this.counterStatus === 3){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowStatusDown.style.borderColor = "#D3D3D3";
                    this.counterStatus = 0;
                }
            },
            OnStatusLectorClick(){
                this.counterStatusLector++;

                let arrowStatusLectorUp = document.getElementById("arrowStatusLectorUp");
                let arrowStatusLectorDown = document.getElementById("arrowStatusLectorDown");


                if(this.counterCompany !== 0){
                    this.ChangeColorsArrows();
                    this.counterCompany = 0;
                }
                if(this.counterEnvironment !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterTitle !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterStatus !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterSpecialization !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterDate !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }

                if(this.counterStatusLector === 1){
                    this.assignmentList.sort((a,b) => a.teacherStatus.toLowerCase() > b.teacherStatus.toLowerCase() ? -1 : 1);
                    arrowStatusLectorUp.style.borderColor = "#000000";
                    arrowStatusLectorDown.style.borderColor = "#D3D3D3"
                }else if(this.counterStatusLector === 2){
                    this.assignmentList.sort((a,b) => a.teacherStatus.toLowerCase() > b.teacherStatus.toLowerCase() ? 1 : -1);
                    arrowStatusLectorUp.style.borderColor = "#D3D3D3";
                    arrowStatusLectorDown.style.borderColor = "#000000";
                }else if(this.counterStatusLector === 3){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowStatusLectorDown.style.borderColor = "#D3D3D3";
                    this.counterStatusLector = 0;
                }
            },
            OnDateClick(){
                this.counterDate++;

                let arrowDateUp = document.getElementById("arrowDateUp");
                let arrowDateDown = document.getElementById("arrowDateDown");


                if(this.counterCompany !== 0){
                    this.ChangeColorsArrows();
                    this.counterCompany = 0;
                }
                if(this.counterEnvironment !== 0){
                    this.ChangeColorsArrows();
                    this.counterEnvironment = 0;
                }

                if(this.counterTitle !== 0){
                    this.ChangeColorsArrows();
                    this.counterTitle = 0;
                }
                if(this.counterStatus !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatus = 0;
                }

                if(this.counterStatusLector !== 0){
                    this.ChangeColorsArrows();
                    this.counterStatusLector = 0;
                }

                if(this.counterSpecialization !== 0){
                    this.ChangeColorsArrows();
                    this.counterDate = 0;
                }

                if(this.counterDate === 1){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? -1 : 1);
                    arrowDateUp.style.borderColor = "#D3D3D3";
                    arrowDateDown.style.borderColor = "#000000";
                }else if(this.counterDate === 2){
                    this.assignmentList.sort((a,b) => a.dateCreated > b.dateCreated ? 1 : -1);
                    arrowDateUp.style.borderColor = "#D3D3D3";
                    arrowDateDown.style.borderColor = "#D3D3D3";
                    this.counterDate = 0;
                }
            },
            ChangeColorsArrows(){
                let arrows = document.getElementsByTagName("i");
                for(let i = 0; i < arrows.length; i++){
                    arrows[i].style.borderColor = "#D3D3D3";
                }
            },
            FillFilterEnvironments(){
                let filter = document.getElementById("selectFilterEnvironment");
                let options = [];
                let defaultOption = ["Omgeving"];
                for(let i = 0; i < this.assignmentList.length; i++){
                    if(this.assignmentList[i].enviroments.toLowerCase().includes(",")){
                        let test = this.assignmentList[i].enviroments.split(",");
                        for(let i = 0; i < test.length; i++){
                            if(test[i] !== null && test[i] !== ""){
                                if(!(options.includes(test[i]))){
                                    options.push(test[i]);
                                }
                            }
                        }
                    }else{
                        options.push(this.assignmentList[i].enviroments);
                    }
                }
                defaultOption = defaultOption.map(o => `<option selected value value=${o}>${o}</option>`);
                filter.innerHTML = defaultOption + options.map(option => `<option value=${option.toLowerCase()}>${option}</option>`).sort((a,b) => a > b ? 1 : -1);
            }
        }
    }
</script>

<style scoped>
.assignments {
    width: 100%;
    margin: 15px;
}
.header {
    width: 100%;
    border-bottom: 1px solid rgba(0, 0, 0, 0.2);
}
.assignment {
    padding: 15px;
    border-bottom: 1px solid rgba(0, 0, 0, 0.2);
}
.noassignments {
    text-align: center;
    padding-top: 50px;
    font-weight: bolder;
    font-size: 1.5em;
}
i {
    border: solid #D3D3D3;
    border-width: 0 2px 2px 0;
    display: inline-block;
    padding: 2px;
}
.up {
    transform: rotate(-135deg);
    -webkit-transform: rotate(-135deg);
}
.down {
    transform: rotate(45deg);
    -webkit-transform: rotate(45deg);
}
.arrowsDiv {
    display: inline-block;
    margin-top: 2px;
    padding: 3px;
}
th:hover{
    cursor: pointer;
}
.arrowsPosition {
    position: absolute;
    margin-top: 5px;
}
.sortAndFilterOptions {
    display: flex;
    margin: 10px;
    height: 25px;
}

.searchBox{
    height: 25px;
    border: 1px solid #575756;
    box-sizing: border-box;
    padding: 5px;
    border-radius: 2px;
    margin-right: 10px;
    margin-left: 10px;
}
select{
    margin: 0 10px 0 10px;
    padding: 2px;
    border-radius: 2px;
    border: 1px solid #575756;
}
</style>