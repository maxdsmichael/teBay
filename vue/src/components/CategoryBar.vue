<template>
<div class="category-container">
  <div v-for="cat in item.categories" :key="cat">
    {{cat}}
  </div>
</div>

</template>

<script>
export default {
    props: {
        item_ID: Number
    },
    data() {
        return {
            item: this.$store.state.listOfItems.find( i => i.item_ID === this.item_ID)
        }
    }
}
</script>

<style scoped>
.category-container {
    display: flex; 
    justify-content: space-between;
    flex-wrap: wrap;
    flex-direction: flex-start;
}
.category-container > div {
    padding: 5px;
    margin: 5px;
    border: 2px solid #1b262c;
    border-radius: 5px;
}
</style>