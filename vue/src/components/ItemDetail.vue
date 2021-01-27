<template>
  <div id="item-detail">
    <div class="loading" v-if="isLoading">
      <img src="../assets/colorWheel.gif" />
    </div>
    <button
      id="please-log-in"
      v-show="this.$store.state.token == ''"
      v-on:click="() => this.$router.push({name: 'login'})"
    >Log in to Place a Bid</button>

    <a href="#bid-form">
      <div
        id="bid-message"
        :class="{'top-bid-message': topBidOfUser === 1, 'out-bid-message': topBidOfUser === 2}"
        v-show="this.$store.state.isAuctionOpen"
      >
        <h5 id="top-bid-message" v-if="topBidOfUser === 1">Winning!</h5>
        <h5 id="out-bid-message" v-if="topBidOfUser === 2">Losing!</h5>
        <h5 id="no-bid-message" v-if="topBidOfUser === 0"></h5>
      </div>
    </a>
    <h1>{{item.title}}</h1>
    <img :src="item.pic" :alt="item.title" />
    <h3>Donated by: {{item.donor}}</h3>
    <h2>{{item.subtitle}}</h2>
    <p>{{item.description}}</p>

    <!-- <category-bar :item_ID="this.item.item_ID" /> -->

    <bid-form :topBid="topBid" :item_ID="item_ID" :isStart="isStart" @bidPlaced="refreshItem()" />
    <h1 v-show="!this.$store.state.isAuctionOpen">The Auction has ended.</h1>
    <table>
      <thead>
        <tr>
          <th>Bid Amount</th>
          <th>UserName</th>
          <th>Time</th>
        </tr>
      </thead>
      <tr v-for="(bid) in bids" :key="bid.bid_ID" class="bid-container">
        <td>${{bid.amount}}</td>
        <td>{{bid.user_Name}}&nbsp;</td>
        <td>{{bid.time_Placed}}</td>
      </tr>
    </table>
    <button
      href="javascript:void(0)"
      id="bid-button"
      @click="simple_toggle(default_limit, item.bids.length)"
      v-if="item.bids.length > 5"
    >{{ limit_by===5?'Show all bids': 'Show \'Top 5\' Bids'}}</button>
    <p v-if="item.bids.length == 0">There are currently no bids</p>
  </div>
</template>

<script>
import BidForm from "./BidForm.vue";
import api from "@/services/ApiService.js";

export default {
  components: {
    BidForm,
  },
  title() {
    return `Auction Item - ${this.item.title}`;
  },
  data() {
    return {
      item: Object,
      default_limit: 5,
      limit_by: 5,
      isLoading: true,
      //item: this.$store.state.listOfItems.find(i => i.item_ID === this.item_ID),
    };
  },
  props: {
    item_ID: Number,
  },
  computed: {
    bids() {
      if (this.item.bids != undefined) {
        return this.item.bids.slice(0, this.limit_by);
      } else {
        return null;
      }
    },
    topBid() {
      if (this.item.bids != undefined) {
        return this.item.bids.length > 0
          ? this.item.bids[0].amount
          : this.item.starting_Bid;
      } else {
        return 0;
      }
    },
    topBidOfUser() {
      // Find the first bid from user
      if (this.item.bids == undefined) {
        return 0;
      }
      let userBid = this.item.bids.find(
        (bid) => bid.user_Name === this.$store.state.user.username
      );
      if (!userBid || userBid == undefined) {
        return 0; //You haven't bid
      }
      if (userBid === this.item.bids[0]) {
        return 1; //`You have top bid!`
      }
      return 2; //`You've been outbid!`
    },
    isStart() {
      if (this.item.bids != undefined) {
        return this.item.bids.length < 1;
      } else {
        return true;
      }
    },
  },
  methods: {
    simple_toggle(default_limit, filters_length) {
      this.limit_by =
        this.limit_by === default_limit ? filters_length : default_limit;
    },
    refreshItem() {
      api
        .getSingleItem(this.item_ID)
        .then((r) => {
          if (r.status === 200) {
            this.item = r.data;
          }
        })
        .catch((e) => {
          if (e.response) {
            console.error(e.response);
          } else if (e.request) {
            console.error(e.request);
          } else {
            console.error("There was an error!");
          }
        });
    },
  },
  created() {
    this.refreshItem();
    api
      .getSingleItem()
      .then((response) => {
        this.item = response.data;
      })
      .finally(() => {
        setTimeout(() => {
          this.isLoading = false;
          //your code to be executed after 1 second
        }, 500);
      });
  },
};
</script>

<style scoped>
#item-detail {
  text-align: center;
}
#item-detail h1 {
  font-weight: 700;
  margin-bottom: 0px;
  padding: 15px;
}
.loading {
  width: 100vw;
  height: 100vh;
  position: absolute;
  top: 0;
  left: 0;
  z-index: 1000;
}
.loading img {
  width: 100%;
  height: 100%;
}
#item-detail h3 {
  margin-top: 0px;
  font-size: 0.75em;
  text-align: right;
}
#item-detail h2 {
  font-size: 1.25em;
}
#item-detail img {
  max-width: 95%;
  border-radius: 8px;
  box-shadow: 1px 1px 4px rgba(192, 192, 192, 0.356);
}
#please-log-in {
  width: 100%;
  background-color: red;
}
#bid-button {
  color: azure;
}
table {
  border-spacing: 10px 5px;
  overflow-wrap: break-word;
  max-width: 100%;
}
#bid-message {
  text-align: center;
  color: #1b262c;
  padding: 8px;
  border-radius: 8px;
}
#bid-message h5 {
  margin: 2px;
  cursor: pointer;
  font-size: 1.15em;
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
template a:visited {
  color: rgb(243, 243, 243);
  text-decoration: none;
}
</style>