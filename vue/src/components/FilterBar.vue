<template>
  <div id="filter" class="menu-bar">
      <button v-on:click="toggleVisible">{{isFilterVisible ? 'FILTER BY CATEGORY' : 'HIDE FILTER'}}</button>
      <div v-show="!isFilterVisible">

      <ul>
          <li v-for="category in categories" :key="category.categoryID" >
              
              <input type="checkbox" :name="category.name" :value="category.name" v-model="filterSelections" v-on:change="updateFilter">
              <label :for="category.name"> {{category.name}}</label>
          </li>
      </ul>
      <!-- <label for="searchTitle">Title: </label>
      <input type="text" name="searchTitle" v-model="searchTitle" />  -->  
      </div>
      

  </div>
</template>

<script>
// TODO: search by title?
export default {
    data() {
        return {          
            searchTitle: '',
            filterSelections: [],
            isFilterVisible: true,
        }
    },
    computed: {
        categories() {
            return this.$store.state.listOfCategories;
        },

    },
    methods: {
        updateFilter(){
            this.$store.commit('UPDATE_FILTER_CATEGORIES', this.filterSelections);
        },
        toggleVisible() {
            this.isFilterVisible = !this.isFilterVisible;
        }
    }
}
</script>

<style scoped>
div#filter  ul {
    display: flex;
    flex-wrap: wrap;
}
div#filter li {
    list-style-type: none;
   /*  list-style-position: outside; */
   margin: 10px;
}
</style>