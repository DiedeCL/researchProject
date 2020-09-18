<template>
    <table class="assignment">
        <col width="20%" />
        <col width="10%" />
        <col width="15%" />
        <col width="25%" />
        <col width="10%" />
        <col width="10%" />
        <tr class="tr" v-on:click="extraInfo(assignment.id)">
            <td>{{assignment.specialization}}</td>
            <td>{{assignment.companyName}}</td>
            <td v-if="assignment.enviroments.length <20">{{assignment.enviroments}}</td>
            <td v-else>{{assignment.enviroments.substring(0,20)+"..."}}</td>
            <td>{{assignment.title}}</td>
            <td><statusIndicator v-bind:status="assignment.teacherStatus.toLowerCase()" /></td>
            <td>{{assignment.dateCreated.split('T')[0]}}</td>
        </tr>
    </table>
</template>

<script>
    import statusIndicator from "./AssignmentStatusComponent";

    export default {
        name: "Assignment",
        props: ["assignment"],
        components: {
            statusIndicator
        },
        methods: {
            extraInfo(e) {
                this.$router.push('/extrainfo?id=' + e);
            }
        }
    }
</script>

<style scoped>
    .tr:hover {
        cursor: pointer;
        background-color: #f5f5f5;
    }
    .assignment {
        text-align: center;
        width: 100%;
        border-collapse: collapse;
    }
</style>