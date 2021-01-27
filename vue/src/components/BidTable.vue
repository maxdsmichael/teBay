<template>
  <div id="bid-table">
<table>
  <thead>
    <tr>
      <th>Bid Amount</th>
      <th id="i-n-head">Item Name</th>
      <th>Time</th>
    </tr>
  </thead>
   <tr v-for="(bid) in bids" :key="bid.bid_ID" class="bid-container" >      
       <td> ${{bid.amount}}</td>
        <td id="bt-it">{{getItemTitle(bid.item_ID)}}&nbsp;</td>
        <td>{{bid.time_Placed}}</td>    
     </tr>
    </table>
  </div>
</template>

<script>
export default {
props: {
    bids: Array,
},
methods: {
  getItemTitle(itemID){
    if (this.$store.state.listOfItems === null || this.$store.state.listOfItems.length === 0){
      this.$store.dispatch('getAllItems').then(() => {
         let item = this.$store.state.listOfItems.find(i => i.item_ID === itemID);
         if (!item){
           return ``;
         }
    return item.title;
      });
    }
    else {
      let item = this.$store.state.listOfItems.find(i => i.item_ID === itemID);
    return item.title;
    }
  }
}
}
</script>

<style scoped>

table {
  border-spacing: 15px 5px;
}
thead {
   text-decoration: underline;
}
#i-n-head {
  display: flex;
  flex-direction: column;
  text-align: center;
}
#bt-it {
    display: flex;
  flex-direction: column;
  align-content: center;
  text-align: center;
}
@media only screen and (max-width: 500px) {
  table {
  border-spacing: 10px 5px;

  max-width: 100%;
  font-size: 60%;
 overflow-wrap: break-word;
}
}
</style>