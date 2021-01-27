<template>
  <ul id="item-list">
              <div class="loading" v-if="isLoading">
        <img src="../assets/colorWheel.gif" />
      </div>
      <li v-for="item in items" :key="item.item_ID">
        <router-link tag="a" :to="{name: 'item', params: {itemID: item.item_ID}}" >
        <item-card :item_ID="item.item_ID" />
        </router-link>
      </li>
  </ul>
</template>
<!-- this.$store.getters.filteredItems()" -->
<script>
import itemCard from '@/components/ItemCard.vue'
import api from "@/services/ApiService.js";


export default {
  data() {
    return {
      isLoading: true,
    }
  },
  computed: {
    items(){
      return this.$store.getters.filteredItems;
    },
  }, 
  components: {
    itemCard
  },
   created() {
    api.getAllItems().then(response => {
    this.item = response.data;
  }).finally(() => { 
    setTimeout(() => {
      this.isLoading = false;
  //your code to be executed after 1 second
}, 500);
     })
  } 
}
</script>

<style>
#item-list {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-evenly;
  padding: 0px;
  
}
#item-list >  li {
  list-style-type: none;
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

</style>