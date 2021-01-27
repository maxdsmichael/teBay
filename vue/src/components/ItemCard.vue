<template>
  <div id="item-card">
    <h4>{{currentBid}}</h4>
    <div
      id="bid-message"
      :class="{'top-bid-message': topBid === 1, 'out-bid-message': topBid === 2}"
    >
      <h5 id="top-bid-message" v-if="topBid === 1">Winning!</h5>
      <h5 id="out-bid-message" v-if="topBid === 2">Losing!</h5>
      <h5 id="no-bid-message" v-if="topBid === 0"></h5>
    </div>
    <h3>{{item.title}}</h3>

    <img :src="item.pic" :alt="item.title" />
    <p>{{item.subtitle}}</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      // TODO: display top bid instead of starting bid if there are bids
      // TODO: display how many bids have benn placed
    };
  },
  computed: {
    item() {
      return this.$store.state.listOfItems.find(
        (i) => i.item_ID === this.item_ID
      );
    },
    currentBid() {
      if (this.item.bids.length > 0) {
        return `$${this.item.bids[0].amount}`;
      } else {
        return `$${this.item.starting_Bid}`;
      }
    },
    topBid() {
      // Find the first bid from user
      let userBid = this.item.bids.find(
        (bid) => bid.user_Name === this.$store.state.user.username
      );
      if (!userBid) {
        return 0; //You haven't bid
      }
      if (userBid === this.item.bids[0]) {
        return 1; //`You have top bid!`
      }
      return 2; //`You've been outbid!`
    },
  },
  props: {
    item_ID: Number,
  },
  components: {},
};
</script>

<style scoped>
#item-card {
  height: 560px;
  width: 400px;
  padding: 0px;
  margin: 20px 25px;
  position: relative;
}
/* TODO: make breakpoint for small screens */
#item-card.has-bid {
  border: 5px solid greenyellow;
}
#item-card > h3 {
  text-align: center;
  font-weight: 400;
  font-size: 2em;
  width: 90%;
  color: #1b262c;
  background-image: linear-gradient(
    to bottom left,
    rgba(192, 192, 192),
    rgba(167, 164, 164)
  );
  /* box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356); */
  border-radius: 5px;
  padding: 10px;
  margin-top: 0px;
  margin-bottom: -4px;
}
#item-card > img {
  max-width: 95%;
  max-height: 100%;
  /* border-radius: 10px; */
  z-index: -1;
  /*  box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356); */
}
#item-card > p {
  width: 90%;
  color: #1b262c;
  background-image: linear-gradient(
    to bottom left,
    rgba(192, 192, 192),
    rgba(167, 164, 164)
  );
  box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356);
  border-radius: 5px;
  padding: 10px;
  margin-top: -8px;
}
#card-stat {
  grid-area: stats;
  display: flex;
  flex-direction: column;
}
.top-bid-message {
  background-image: linear-gradient(
    to bottom left,
    rgb(0, 200, 0),
    rgb(2, 134, 2)
  );
  box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356);
}
.out-bid-message {
  background-image: linear-gradient(
    to bottom left,
    rgb(255, 87, 87),
    rgb(243, 2, 2)
  );
  box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356);
}

#item-card h4 {
  color: #1b262c;
  font-size: 1.75em;
  display: inline-block;
  z-index: -1;
  position: relative;
  top: 10px;
  background-image: linear-gradient(to top, rgba(192, 192, 192), rgb(148, 148, 148));
  box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356);
  padding: 8px;
  border-radius: 8px;
}

#bid-message {
  text-align: center;
  color: #1b262c;
  display: inline-block;
  position: relative;
  right: 4px;
  top: 6px;
  z-index: -2;
  padding: 8px;
  border-radius: 8px;
}

#bid-message h5 {
  margin: 2px;
}

@media screen and (max-width: 480px) {
  #item-card {
    width: 300px;
    height: 420px;
    margin-bottom: auto;
  }

  /*   #bid-message {
  top: 140px;  
}
#item-card h4 {
  top: 140px;
} */
}
</style>