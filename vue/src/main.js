import Vue from 'vue'
import Auction from './Auction.vue'
import router from './router/index'
import store from './store/index'
import axios from 'axios'
import titleMixin from './mixins/titleMixin'
Vue.mixin(titleMixin)

Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

new Vue({
  router,
  store,
  render: h => h(Auction)
}).$mount('#auction')

