<template>
  <div id="current-bid-table">
          <div class="loading" v-if="isLoading">
        <img src="../assets/colorWheel.gif" />
      </div>
<table>
  <thead>
    <tr>
      <th>Bid Amount</th>
      <th id="cbtin-head">Item Name</th>
      <th>Time</th>
      <!-- <th id="jump-header">Jump To Item</th> -->
    </tr>
  </thead>
   <tr v-for="(bid) in currentBids" :key="bid.bid_ID" class="bid-container" >      
       <td id="b-amount"> ${{bid.amount}}</td>
<td><router-link id="item-title"  :to="{name: 'item', params: {itemID: bid.item_ID}}"
    >{{getItemTitle(bid.item_ID)}}</router-link></td>        
    <td id="b-time">{{bid.time_Placed}}</td>
        <!-- <td><router-link id="jump-button" tag="button" :to="{name: 'item', params: {itemID: bid.item_ID}}"
    >{{getItemTitle(bid.item_ID)}}</router-link></td>     -->
     </tr>
    </table>
  </div>
</template>

<script>
import APIService from "@/services/ApiService.js";
export default {
    data() {
    return {
      isLoading: true,
    }
  },
props: {
    currentBids: Array,
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
},
created() {
      APIService.getHighestBidPerItemForSingleUser().then(response => {
    this.bid = response.data;
  }).finally(() => { 
    setTimeout(() => {
      this.isLoading = false;
  //your code to be executed after 1 second
}, 500);
     });
}
}
</script>

<style scoped>

table {
  border-spacing: 15px 5px;
  justify-content: center;
 align-content: right;
 
}
thead {
   text-decoration: underline;
}
.loading {
  width: 100vw;
  height: 100vh;
  position: absolute; top: 0; left: 0;
   z-index: 1000;
}
.loading img {
  width: 100%;
  height: 100%;
 
}
#cbtin-head {
    display: flex;
  flex-direction: column;
  text-align: center;
}
#item-title{
  display: flex;
  flex-direction: column;
  align-content: center;
  justify-content: right;
  
 
  border-width: 5px;
    color: #fff;
  text-align: center;
  text-shadow: 0 0 5px #fff, 0 0 10px #fff, 0 0 15px #0073e6, 0 0 20px #0073e6, 0 0 25px #0073e6, 0 0 30px #0073e6, 0 0 35px #0073e6;
  
  animation: glow .75s ease-in-out infinite alternate;
    color: #e7dfd5;
        font-family: Arial, Helvetica, sans-serif; font-size: 20px; font-style: normal; font-variant: normal; ;
    text-align: center;


  color: #fff;
  text-align: center;
 text-shadow: 0 0 5px #fff, 0 0 10px #fff, 0 0 15px #0073e6, 0 0 20px #0073e6, 0 0 25px #0073e6, 0 0 30px #0073e6, 0 0 35px #0073e6;
}
#b-amount {
  font-size: 20px;
}
#b-time {
  font-size: 20px;
}
/* #jump-button {
  max-width: 100%;
  color: #fff;
  text-align: center;
  text-shadow: 0 0 5px #fff, 0 0 10px #fff, 0 0 15px #0073e6, 0 0 20px #0073e6, 0 0 25px #0073e6, 0 0 30px #0073e6, 0 0 35px #0073e6;
  
  animation: glow .75s ease-in-out infinite alternate;
    color: #e7dfd5;
        font-family: Arial, Helvetica, sans-serif; font-size: 50px; font-style: normal; font-variant: normal; ;
    text-align: center;


  color: #fff;
  text-align: center;
 text-shadow: 0 0 5px #fff, 0 0 10px #fff, 0 0 15px #0073e6, 0 0 20px #0073e6, 0 0 25px #0073e6, 0 0 30px #0073e6, 0 0 35px #0073e6;
} */

@media only screen and (max-width: 500px) {
  table {
  border-spacing: 10px 5px;

  max-width: 100%;
  font-size: 60%;
 overflow-wrap: break-word;
}
#jump-button {
  width: 40%;
  height: 30px;
  font-size: 10px;
  display: flex;
  justify-content: center;

}
#jump-header {
  
  justify-content: center;
  overflow-wrap: break-word;
}
}
</style>