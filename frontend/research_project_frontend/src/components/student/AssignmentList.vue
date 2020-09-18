<template>
  <div class="assignments">
    <div class="sortAndFilterOptions">
      <select id="selectFilterEnvironment" v-model="selectedFilterEnvironments">
      </select>

      <div>
        <input id="searchOnTitle" type="text" placeholder="Zoeken Titel" title="Zoeken op titel" class="searchBox" v-model="searchOnTitle">
      </div>
      <div>
        <input id="searchOnResearchTheme" type="text" placeholder="Zoeken Onderzoeks Thema" title="Zoeken op onderzoeks thema" class="searchBox"  v-model="searchOnResearchTheme">
      </div>
      <div>
        <input id="searchOnCompany" type="text" placeholder="Zoeken Bedrijf" title="Zoeken op bedrijf" class="searchBox" v-model="searchOnCompany">
      </div>
      <div>
        <input id="searchOnLocation" type="text" placeholder="Zoeken Locatie" title="Zoeken op locatie" class="searchBox"  v-model="searchOnLocation">
      </div>
    </div>
    <table class="header">
      <col width="20%" />
      <col width="20%" />
      <col width="20%" />
      <col width="20%" />
      <tr class="tr">
        <th v-on:click="OnTitleClick">Titel
          <div id="arrowTitleBoth" class="arrowsDiv">
            <i id="arrowTitleUp" class="up arrowsPosition"></i>
            <i id="arrowTitleDown" class="down"></i>
          </div>
        </th>
        <th v-on:click="OnEnvironmentClick">Omgevingen
          <div id="arrowEnvironmentBoth" class="arrowsDiv">
            <i id="arrowEnvironmentUp" class="up arrowsPosition"></i>
            <i id="arrowEnvironmentDown" class="down"></i>
          </div>
        </th>
        <th v-on:click="OnResearchThemeClick">Onderzoeks thema
          <div id="arrowResearchThemeBoth" class="arrowsDiv">
            <i id="arrowResearchThemeUp" class="up arrowsPosition"></i>
            <i id="arrowResearchThemeDown" class="down"></i>
          </div>
        </th>
        <th v-on:click="OnCompanyClick">Bedrijf
          <div id="arrowCompanyBoth" class="arrowsDiv">
            <i id="arrowCompanyUp" class="up arrowsPosition"></i>
            <i id="arrowCompanyDown" class="down"></i>
          </div>
        </th>
        <th v-on:click="OnLocationClick">Locatie
          <div  id="arrowLocationBoth" class="arrowsDiv">
            <i id="arrowLocationUp" class="up arrowsPosition"></i>
            <i id="arrowLocationDown" class="down"></i>
          </div>
        </th>
      </tr>
    </table>
    <div v-if="assignmentList.length > 0">
      <assignment
              class="assignment"
              v-for="(item, index) in computed_items"
              v-bind:key="item.id"
              v-bind:index="index"
              v-bind:assignment="item"
      />
    </div>
    <div v-else-if="error.length > 0">{{error}}</div>
    <div v-else class="noassignments">
      <p>Geen opdrachten om weer te geven.</p>
    </div>
  </div>
</template>

<script>
  import assignment from "./Assignment";

  export default {
    name: "assignmentlist",
    props: ["account"],
    components: {
      assignment
    },
    data() {
      return {
        assignmentList: [],
        error: "",
        requestOptions: {
          method: "POST",
          mode: "cors",
          body: JSON.stringify({
            "Specialization": `${JSON.parse(localStorage.getItem('user')).specialization}`
          }),
          headers: {
            Authorization: `Bearer ${
                    JSON.parse(localStorage.getItem("user")).token
            }`,
            "Acces-Control-Allow-Origin": "*",
            "Accept": "application/json",
            "Content-Type": "application/json"
          }
        },
        counterCompany: 0,
        counterLocation: 0,
        counterTitle: 0,
        counterEnvironment: 0,
        counterResearchTheme: 0,
        selectedFilterEnvironments : "",
        searchOnCompany: "",
        searchOnTitle : "",
        searchOnLocation: "",
        searchOnResearchTheme : ""
      }
    },
    mounted() {
      fetch(
              "https://localhost:5001/api/assignments/GetApprovedInternshipAssingmentBySpecialization",
              this.requestOptions
      ).then(response =>
              response
                      .json()
                      .then(assignments => {
                        if (!response.ok) {
                          if (response.status == 400) {
                            this.error = "Kan opdrachten niet opvragen van de servers";
                          }
                          return Promise.reject(this.error);
                        }
                        assignments.forEach(assignment => {
                          console.log(assignment)
                          this.assignmentList.push(assignment);
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
        let searchOnTitle = this.searchOnTitle;
        let filterEnvironment = this.selectedFilterEnvironments;
        let searchOnCompany = this.searchOnCompany;
        let searchOnLocation = this.searchOnLocation;
        let searchOnResearchTheme = this.searchOnResearchTheme;
        let list = this.assignmentList;
        if(searchOnTitle === "" && filterEnvironment === "Omgeving" && searchOnCompany  ==="" && searchOnLocation === "" && searchOnResearchTheme === ""){
          return this.assignmentList;
        }else{
          if(searchOnTitle !== ""){
            list = list.filter((assignment) => {
              return assignment.title.toLowerCase().match(searchOnTitle.toLowerCase());
            });
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
          if(searchOnLocation !== ""){
            list = list.filter((assignment) => {
              return assignment.township.toLowerCase().match(searchOnLocation.toLowerCase());
            });
          }
          if(searchOnResearchTheme !== ""){
            list = list.filter((assignment) => {
              return assignment.researchTheme.toLowerCase().match(searchOnResearchTheme.toLowerCase());
            });
          }
        }
        return list;
      }
    },
    methods : {
      OnEnvironmentClick() {
        this.counterEnvironment++;
        console.log(this.assignmentList);
        let arrowEnvironmentUp = document.getElementById("arrowEnvironmentUp");
        let arrowEnvironmentDown = document.getElementById("arrowEnvironmentDown");

        if (this.counterCompany !== 0) {
          this.ChangeColorsArrows();
          this.counterCompany = 0;
        }
        if (this.counterSpecialization !== 0) {
          this.ChangeColorsArrows();
          this.counterSpecialization = 0;
        }

        if (this.counterTitle !== 0) {
          this.ChangeColorsArrows();
          this.counterTitle = 0;
        }

        if (this.counterResearchTheme !== 0) {
          this.ChangeColorsArrows();
          this.counterResearchTheme = 0;
        }

        if (this.counterLocation !== 0) {
          this.ChangeColorsArrows();
          this.counterLocation = 0;
        }

        if (this.counterEnvironment === 1) {
          this.assignmentList.sort((a, b) => a.enviroments.toLowerCase() > b.enviroments.toLowerCase() ? 1 : -1);
          arrowEnvironmentUp.style.borderColor = "#000000";
          arrowEnvironmentDown.style.borderColor = "#D3D3D3"
        } else if (this.counterEnvironment === 2) {
          this.assignmentList.sort((a, b) => a.enviroments.toLowerCase() > b.enviroments.toLowerCase() ? -1 : 1);
          arrowEnvironmentUp.style.borderColor = "#D3D3D3";
          arrowEnvironmentDown.style.borderColor = "#000000";
        } else if (this.counterEnvironment === 3) {
          this.assignmentList.sort((a, b) => a.dateCreated > b.dateCreated ? 1 : -1);
          arrowEnvironmentDown.style.borderColor = "#D3D3D3";
          this.counterEnvironment = 0;
        }
      },
      OnTitleClick() {
        this.counterTitle++;

        let arrowTitleUp = document.getElementById("arrowTitleUp");
        let arrowTitleDown = document.getElementById("arrowTitleDown");

        if (this.counterCompany !== 0) {
          this.ChangeColorsArrows();
          this.counterCompany = 0;
        }
        if (this.counterSpecialization !== 0) {
          this.ChangeColorsArrows();
          this.counterSpecialization = 0;
        }

        if (this.counterEnvironment !== 0) {
          this.ChangeColorsArrows();
          this.counterEnvironment = 0;
        }

        if (this.counterResearchTheme !== 0) {
          this.ChangeColorsArrows();
          this.counterResearchTheme = 0;
        }

        if (this.counterLocation !== 0) {
          this.ChangeColorsArrows();
          this.counterLocation = 0;
        }

        if (this.counterTitle === 1) {
          this.assignmentList.sort((a, b) => a.description.toLowerCase() > b.description.toLowerCase() ? 1 : -1);
          arrowTitleUp.style.borderColor = "#000000";
          arrowTitleDown.style.borderColor = "#D3D3D3"
        } else if (this.counterTitle === 2) {
          this.assignmentList.sort((a, b) => a.description.toLowerCase() > b.description.toLowerCase() ? -1 : 1);
          arrowTitleUp.style.borderColor = "#D3D3D3";
          arrowTitleDown.style.borderColor = "#000000";
        } else if (this.counterTitle === 3) {
          this.assignmentList.sort((a, b) => a.dateCreated > b.dateCreated ? 1 : -1);
          arrowTitleDown.style.borderColor = "#D3D3D3";
          this.counterTitle = 0;
        }
      },
      OnCompanyClick() {
        this.counterCompany++;
        let arrowCompanyUp = document.getElementById("arrowCompanyUp");
        let arrowCompanyDown = document.getElementById("arrowCompanyDown");

        if (this.counterEnvironment !== 0) {
          this.ChangeColorsArrows();
          this.counterEnvironment = 0;
        }
        if (this.counterSpecialization !== 0) {
          this.ChangeColorsArrows();
          this.counterSpecialization = 0;
        }

        if (this.counterTitle !== 0) {
          this.ChangeColorsArrows();
          this.counterTitle = 0;
        }

        if (this.counterResearchTheme !== 0) {
          this.ChangeColorsArrows();
          this.counterResearchTheme = 0;
        }

        if (this.counterLocation !== 0) {
          this.ChangeColorsArrows();
          this.counterLocation = 0;
        }
        if (this.counterCompany === 1) {
          this.assignmentList.sort((a, b) => a.companyName > b.companyName ? 1 : -1);
          arrowCompanyUp.style.borderColor = "#000000";
          arrowCompanyDown.style.borderColor = "#D3D3D3"
        } else if (this.counterCompany === 2) {
          this.assignmentList.sort((a, b) => a.companyName > b.companyName ? -1 : 1);
          arrowCompanyUp.style.borderColor = "#D3D3D3";
          arrowCompanyDown.style.borderColor = "#000000";
        } else if (this.counterCompany === 3) {
          this.assignmentList.sort((a, b) => a.dateCreated > b.dateCreated ? 1 : -1);
          arrowCompanyDown.style.borderColor = "#D3D3D3";
          this.counterCompany = 0;
        }
      },
      OnResearchThemeClick() {
        this.counterResearchTheme++;

        let arrowResearchThemeUp = document.getElementById("arrowResearchThemeUp");
        let arrowResearchThemeDown = document.getElementById("arrowResearchThemeDown");

        if (this.counterCompany !== 0) {
          this.ChangeColorsArrows();
          this.counterCompany = 0;
        }
        if (this.counterSpecialization !== 0) {
          this.ChangeColorsArrows();
          this.counterSpecialization = 0;
        }

        if (this.counterTitle !== 0) {
          this.ChangeColorsArrows();
          this.counterTitle = 0;
        }

        if (this.counterEnvironment !== 0) {
          this.ChangeColorsArrows();
          this.counterEnvironment = 0;
        }

        if (this.counterLocation !== 0) {
          this.ChangeColorsArrows();
          this.counterLocation = 0;
        }

        if (this.counterResearchTheme === 1) {
          this.assignmentList.sort((a, b) => a.researchTheme > b.researchTheme ? 1 : -1);
          arrowResearchThemeUp.style.borderColor = "#000000";
          arrowResearchThemeDown.style.borderColor = "#D3D3D3"
        } else if (this.counterResearchTheme === 2) {
          this.assignmentList.sort((a, b) => a.researchTheme > b.researchTheme ? -1 : 1);
          arrowResearchThemeUp.style.borderColor = "#D3D3D3";
          arrowResearchThemeDown.style.borderColor = "#000000";
        } else if (this.counterResearchTheme === 3) {
          this.assignmentList.sort((a, b) => a.dateCreated > b.dateCreated ? 1 : -1);
          arrowResearchThemeDown.style.borderColor = "#D3D3D3";
          this.counterResearchTheme = 0;
        }
      },
      OnLocationClick() {
        this.counterLocation++;

        let arrowLocationUp = document.getElementById("arrowLocationUp");
        let arrowLocationDown = document.getElementById("arrowLocationDown");

        if (this.counterCompany !== 0) {
          this.ChangeColorsArrows();
          this.counterCompany = 0;
        }
        if (this.counterSpecialization !== 0) {
          this.ChangeColorsArrows();
          this.counterSpecialization = 0;
        }

        if (this.counterTitle !== 0) {
          this.ChangeColorsArrows();
          this.counterTitle = 0;
        }

        if (this.counterEnvironment !== 0) {
          this.ChangeColorsArrows();
          this.counterEnvironment = 0;
        }

        if (this.counterResearchTheme !== 0) {
          this.ChangeColorsArrows();
          this.counterResearchTheme = 0;
        }

        if (this.counterLocation === 1) {
          this.assignmentList.sort((a, b) => a.township > b.township ? 1 : -1);
          arrowLocationUp.style.borderColor = "#000000";
          arrowLocationDown.style.borderColor = "#D3D3D3"
        } else if (this.counterLocation === 2) {
          this.assignmentList.sort((a, b) => a.township > b.township ? -1 : 1);
          arrowLocationUp.style.borderColor = "#D3D3D3";
          arrowLocationDown.style.borderColor = "#000000";
        } else if (this.counterLocation === 3) {
          this.assignmentList.sort((a, b) => a.dateCreated > b.dateCreated ? 1 : -1);
          arrowLocationDown.style.borderColor = "#D3D3D3";
          this.counterLocation = 0;
        }
      },
      ChangeColorsArrows() {
        let arrows = document.getElementsByTagName("i");
        for (let i = 0; i < arrows.length; i++) {
          arrows[i].style.borderColor = "#D3D3D3";
        }
      },
      FillFilterEnvironments() {
        let filter = document.getElementById("selectFilterEnvironment");
        let options = [];
        let defaultOption = ["Omgeving"];
        for (let i = 0; i < this.assignmentList.length; i++) {
          if (this.assignmentList[i].enviroments.toLowerCase().includes(",")) {
            let test = this.assignmentList[i].enviroments.split(",");
            for (let i = 0; i < test.length; i++) {
              if (test[i] !== null && test[i] !== "") {
                if (!(options.includes(test[i]))) {
                  options.push(test[i]);
                }
              }
            }
          } else {
            options.push(this.assignmentList[i].enviroments);
          }
        }
        defaultOption = defaultOption.map(o => `<option selected value value=${o}>${o}</option>`);
        filter.innerHTML = defaultOption + options.map(option => `<option value=${option.toLowerCase()}>${option}</option>`).sort((a,b) => a > b ? 1 : -1);
      },
    }
  };
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
  #searchOnResearchTheme{
    width: 190px;
  }
</style>