<template>
  <div>
      <item-detail :item_ID="parseInt(item_ID)"/>

  </div>
</template>

<script>
import ItemDetail from '@/components/ItemDetail.vue';


export default {
    name: 'item',
    data() {
        return {
            item_ID: 0,

        }
    },
    components: {
        ItemDetail,
    },
   /*  computed: {        
        foundItem_ID(){
            return this.$route.params.itemID;
        }
    }, */
    created() {
        this.item_ID = this.$route.params.itemID;
    }
}
</script>

<style>

</style>