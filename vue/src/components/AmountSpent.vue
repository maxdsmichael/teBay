<template>
  <div>
    <h5 id="total-amount-spent">Total Value of All Winning Items: ${{totalAmount}}</h5>
  </div>
</template>

<script>
export default {
//TODO: Bring in the stuff from elsewhere, not the $store!!
  data(){
   return {
     
   }
 },

computed: {
  totalAmount(){
    let items =  this.$store.state.listOfItems
    let topBids = []
    let topBidsOfUser = []
    let total = 0

    items.forEach(i => {
      if (i.bids.length > 0){
      topBids.push(i.bids[0])
      }
      total === 0
      
    })

    topBids.forEach(bid => {
      if (bid.user_Name === this.$store.state.user.username){
        topBidsOfUser.push(bid);
      }
    })

    if (topBidsOfUser.length === 0){
      total === 0
    }
    else {
   total = topBidsOfUser.reduce((reducer, bid) => {
      return reducer + bid.amount;
    }, 0)
    }
    return total;
  },

  
},


}
</script>

<style>
#total-amount-spent {
  font-size: 25px;
}
</style>