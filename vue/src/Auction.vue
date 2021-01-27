<template>
  <div id="auction">
    <auction-header />
    <countdown v-on:startEndOfAuction="endOfAuction()" />
    <!-- <nav id="nav" :class="{'nav-active': isShow}">
      <img
        src="@/assets/icons8-menu-384.png"
        alt="hamburger menu icon"
        id="hamburger-menu"
        "
    />-->
    <div id="nav-icon" :class="{'open' : isShow}" v-on:click="showHide">
      <span></span>
      <span></span>
      <span></span>
      <span></span>
    </div>
    <ul id="nav-menu" v-if="isShow" v-on:click="showHide">
      <router-link class="link-box" tag="li" :to="{ name: 'home' }">
        <a>Home</a>
      </router-link>
      <router-link class="link-box" tag="li" :to="{ name: 'activity' }" v-if="isLoggedIn">
        <a>View My Bid History</a>
      </router-link>
      <router-link class="link-box" tag="li" :to="{ name: 'login' }" v-if="!isLoggedIn">
        <a>Login</a>
      </router-link>
      <router-link class="link-box" tag="li" v-bind:to="{ name: 'logout' }" v-if="isLoggedIn">
        <a>Logout</a>
      </router-link>
      <router-link class="link-box" tag="li" :to="{ name: 'register'}" v-if="!isLoggedIn">
        <a>Register</a>
      </router-link>
    </ul>

    <router-view />
  </div>
</template>

<script>
import AuctionHeader from "./components/AuctionHeader.vue";
import Countdown from "./components/Countdown.vue";
export default {
  components: {
    AuctionHeader,
    Countdown,
  },
  data() {
    return {
      isShow: false,
    };
  },
  computed: {
    isLoggedIn() {
      return this.$store.state.token != "";
    },
  },
  methods: {
    showHide() {
      this.isShow = !this.isShow;
    },
    endOfAuction() {
          this.$store.commit('END_AUCTIONS');
          this.$store.dispatch("getAllWinnings")  
    }
  },
};
</script>

<style>
body {
  /* background-color: #1b262c; */
  height: 100%;
  margin: 0;
  background-repeat: no-repeat;
  background-attachment: fixed;
  text-align: center;
  padding: 10px;
  background-image: linear-gradient(#0f4c75, #1b262c);
}
#auction {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: left;
  color: #e7dfd5;
  max-width: 90%;
  margin: 0 auto 0 auto;
}
#nav-icon {
  display: inline-block;
  overflow: hidden;
  flex-wrap: nowrap;
  justify-content: space-evenly;
  margin-bottom: 10px;
  /* width: 50%; */
}
#nav-icon a {
  padding: 5px;
  margin: 5px;
  color: white;
  font-size: 1.25em;
}
a {
  text-decoration: none;
  color: #28b2fc;
}
#hamburger-menu {
  height: 50px;
  margin: 0px;
  cursor: pointer;
}
.nav-active {
  /* change this color   */
  /* background-color: grey;
border: 1px solid grey; */
  border-radius: 8px;
}
#nav-menu {
  /* display: flex;
  flex-direction: column; */
  background-color: lightgray;
  width: 300px;
  box-shadow: 5px 5px 5px gray;
  border-radius: 5px;
  margin: 0;
  box-sizing: border-box;
  padding: 10px 20px;
  padding-inline-start: 0px;
  -webkit-user-select: none; /* Safari */
  -moz-user-select: none; /* Firefox */
  -ms-user-select: none; /* IE10+/Edge */
  user-select: none; /* Standard */
}
#nav-menu > li {
  list-style: none;
  margin: 10px 10px;
  padding: 15px 0px;
  width: 100%;
}
#nav-menu > li:hover {
  background-color: darkgray;
}
.link-box p {
  padding: 15px;
  margin: 20px;
  background-color: #0eb029;
  cursor: pointer;
  text-transform: uppercase;
}

.link-box {
  border: 1px;
  padding: 5px;
  width: 10%;
  /* border-style: ridge;
  border-color: lightgray;
  width: 300px;
  border-radius: 2px;
  color: #B0690E;
   */
}
button {
	box-shadow:inset 0px 1px 0px 0px #fce2c1;
	background:linear-gradient(to bottom, #ce6336 5%, #d3531a 100%);
	background-color:#d3531a;
	border-radius:10px;
	border:1px solid #d3531a;
	display:inline-block;
	cursor:pointer;
	color:#ffffff;
	font-family:Arial;
	font-size:15px;
	font-weight:bold;
	padding:6px 24px;
	text-decoration:none;
	text-shadow:0px 1px 0px #cc9f52;
  outline: 0 none;
}
button:hover {
	background:linear-gradient(to bottom, #fb9e25 5%, #ffc477 100%);
	background-color:#fb9e25;
}
button:active {
	position:relative;
	top:1px;
}

/* button {
  color: #ff7b00;
  background-color: #ffffff00;
  margin: 5px;

}
button:hover {
  color: #ffffff;
  background-color: #1b262c;
  text-decoration-line: underline;
  margin: 5px;
} */

* {
  margin: 0;
  padding: 0;
}

#nav-icon {
  width: 60px;
  height: 45px;
  position: relative;
  margin: 50px auto;
  -webkit-transform: rotate(0deg);
  -moz-transform: rotate(0deg);
  -o-transform: rotate(0deg);
  transform: rotate(0deg);
  -webkit-transition: 0.5s ease-in-out;
  -moz-transition: 0.5s ease-in-out;
  -o-transition: 0.5s ease-in-out;
  transition: 0.5s ease-in-out;
  cursor: pointer;
}

#nav-icon span {
  display: block;
  position: absolute;
  height: 9px;
  width: 100%;
  background: #d3531a;
  border-radius: 9px;
  opacity: 1;
  left: 0;
  -webkit-transform: rotate(0deg);
  -moz-transform: rotate(0deg);
  -o-transform: rotate(0deg);
  transform: rotate(0deg);
  -webkit-transition: 0.25s ease-in-out;
  -moz-transition: 0.25s ease-in-out;
  -o-transition: 0.25s ease-in-out;
  transition: 0.25s ease-in-out;
}

#nav-icon span:nth-child(1) {
  top: 0px;
}

#nav-icon span:nth-child(2),
#nav-icon span:nth-child(3) {
  top: 18px;
}

#nav-icon span:nth-child(4) {
  top: 36px;
}

#nav-icon.open span:nth-child(1) {
  top: 18px;
  width: 0%;
  left: 50%;
}

#nav-icon.open span:nth-child(2) {
  -webkit-transform: rotate(45deg);
  -moz-transform: rotate(45deg);
  -o-transform: rotate(45deg);
  transform: rotate(45deg);
}

#nav-icon.open span:nth-child(3) {
  -webkit-transform: rotate(-45deg);
  -moz-transform: rotate(-45deg);
  -o-transform: rotate(-45deg);
  transform: rotate(-45deg);
}

#nav-icon.open span:nth-child(4) {
  top: 18px;
  width: 0%;
  left: 50%;
}
</style>