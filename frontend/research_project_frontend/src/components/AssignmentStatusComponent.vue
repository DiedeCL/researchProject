<template>
    <div>
      <p class="review status" v-if="status.toLowerCase()  == 'behandeling'">In {{status}}...</p>
      <p class="approved status" v-if="status.toLowerCase()  == 'goedgekeurd'">{{status}}</p>
      <p class="denied status" v-if="status.toLowerCase() == 'afgekeurd'">{{status}}</p>
    </div>
</template>

<script>
export default {
    name: "statusIndicator",
    props:["status"]
}
</script>

<style scoped>
.status {
    padding: 10px;
    height: 60px;
}
.approved {
  background-color: rgba(88, 165, 24, 0.8);
  color: white;
}
.denied {
  background-color: rgb(165, 24, 24);
  color: white;
}
.review {
  background-color: rgba(0, 0, 0, 0.2);
  color: black;
}
</style>