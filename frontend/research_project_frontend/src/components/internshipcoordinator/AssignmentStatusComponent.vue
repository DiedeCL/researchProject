<template>
    <div>
      <p class="review status" v-if="status.toLowerCase().startsWith('behandeling')">In {{status}}...</p>
      <p class="approved status" v-if="status.toLowerCase().startsWith('goedgekeurd')">{{status}}</p>
      <p class="denied status" v-if="status.toLowerCase().startsWith('afgekeurd')">{{status}}</p>
      <p class="sent status" v-if="status.toLowerCase().startsWith('opdracht verzonden')">Ongelezen</p>
      <p class="sent status" v-if="status.toLowerCase().startsWith('geen lector toegevoegd')">{{status}}</p>
      <p class="review status" v-if="status.toLowerCase().startsWith('lector toegewezen')">{{status}}</p>
      <p class="verschil status" v-if="status.toLowerCase().startsWith('verschillende reviews')">{{status}}</p>
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
  background-color: blue;
  color: white;
}
.sent {
    background-color: rgba(0, 0, 0, 0.2);
    color: black;
}
p{
    height: 60px;
}
.verschil {
    background-color: deeppink;
    color: white;
}
</style>