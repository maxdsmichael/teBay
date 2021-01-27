<template>
  <div id="countdown">
    <ul>
      <li id="days">
        <span>{{days}}</span>
        <div>days</div>
      </li>
      <li id="hours">
        <span>{{hours}}</span>
        <div>hours</div>
      </li>
      <li id="minutes">
        <span>{{minutes}}</span>
        <div>minutes</div>
      </li>
      <li id="seconds">
        <span>{{seconds}}</span>
        <div>seconds</div>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  data() {
    return {
      timeRemaining: "",
      days: "",
      hours: "",
      minutes: "",
      seconds: "",
      counter: 0,
      timer: null,
      countdownPolling: null,
      /*   organizationName: this.$store.state.auctionInfo.orgName,
      endTime: this.$store.state.auctionInfo.endTime */
      //TODO: let's try to make this a countdown
    };
  },
  methods: {
    countdown() {
      const endTime = this.$store.state.auctionInfo.endTime;

      let now = new Date().getTime();
      let t = endTime - now;
      let days = Math.floor(t / (1000 * 60 * 60 * 24));
      let hours = Math.floor((t % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      let minutes = Math.floor((t % (1000 * 60 * 60)) / (1000 * 60));
      let seconds = Math.floor((t % (1000 * 60)) / 1000);

      this.timeRemaining = `${days} days ${hours} hours ${minutes} minutes ${seconds} seconds`;
      this.days = `${days}`;
      this.hours = `${hours}`;
      this.minutes = `${minutes}`;
      this.seconds = `${seconds}`;

      if (seconds < 0){
          this.$emit("startEndOfAuction");
          clearInterval(this.countdownPolling);
      }
    },

  },

  created() {
    this.countdownPolling = setInterval(
      function (vue) {
        vue.countdown();
      },
      1000,
      this
    );
  },
};
</script>

<style>
#countdown li {
  display: inline-block;
  list-style-type: none;
  padding: 1em;
  text-transform: uppercase;
}

#countdown li > span {
  font-size: 2em;
}
#countdown ul {
  width: 30%;
  position: absolute;
  top: 0px;
  right: 10px;
  text-align: center;
  z-index: 2;
  color: #fca028;
  /* 
  background-color: rgba(0, 0, 0, 0.267); */
}

#countdown div {
  position: relative;
}

@media screen and (max-width: 1285px) {
  #countdown ul {
    width: 40%;
  }
}

@media screen and (max-width: 1000px) {
  #countdown ul {
    display: grid;
    grid-template-columns: 1fr 1fr;
    width: 100%;
    position: static;
    align-items: center;
  }

  #countdown li {
    padding: 5px;
    margin: auto;
  }

  #countdown li > span {
    font-size: 1em;
  }
}
</style>