<template>
  <div class="home">
    <filter-bar />
    <amount-spent />
    <h2 id="items-won" v-show="!this.$store.state.isAuctionOpen">{{itemsWon}}</h2>
    <item-list />
  </div>
</template>

<script>
import FilterBar from "@/components/FilterBar.vue";
import ItemList from "@/components/ItemList.vue";
import AmountSpent from "@/components/AmountSpent.vue";

export default {
  name: "home",
  components: {
    FilterBar,
    ItemList,
    AmountSpent,
  },
  computed: { 
    itemsWon() {
      return this.$store.state.listOfItems.length > 0 ? 'Here are the items you won!' : 'The Auction has ended.';
    }
  },
  created() {
    if (this.$store.state.isAuctionOpen) {
      this.$store.dispatch("getAllItems");
      this.$store.dispatch("getAllCategories");
    }
    else {
      this.$store.dispatch("getAllWinnings");
    }
    // This uses an action in Vuex to call API and load to listOfItems
  },
};
</script>

<style>
div.home h2#items-won {
text-align: center;
font-size: 2em;
}
</style>