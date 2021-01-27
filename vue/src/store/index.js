import Vue from 'vue'
import Vuex from 'vuex'
import axios from 'axios'
//import authService from '@/services/AuthService.js';
import apiService from '@/services/ApiService.js'

Vue.use(Vuex)

/*
 * The authorization header is set for axios when you login but what happens when you come back or
 * the page is refreshed. When that happens you need to check for the token in local storage and if it
 * exists you should set the header so that it will be attached to each request
 */

const currentToken = localStorage.getItem('token')
const currentUser = JSON.parse(localStorage.getItem('user'));

if (currentToken != null) {
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}


export default new Vuex.Store({
  state: {
    token: currentToken || '',
    user: currentUser || {},
    isAuctionOpen: true,
    listOfItems: [],
    listOfCategories: [],
    categoriesToFilter: [],
    auctionInfo: {
      // TODO: get this data from database
      orgName: 'TE: Bay',
      endTime: new Date(2020, 8, 14, 10, 6)
    },
    listOfBids: [],
    // Bid at index0 is top bid see if current user name = top user name
  },
  mutations: {
    SET_AUTH_TOKEN(state, token) {
      state.token = token;
      localStorage.setItem('token', token);
      axios.defaults.headers.common['Authorization'] = `Bearer ${token}`
    },
    SET_USER(state, user) {
      state.user = user;
      localStorage.setItem('user', JSON.stringify(user));
    },
    LOGOUT(state) {
      localStorage.removeItem('token');
      localStorage.removeItem('user');
      state.token = '';
      state.user = {};
      axios.defaults.headers.common = {};
    },
    SET_ITEM_LIST(state, list) {
      state.listOfItems = list;
    },
    SET_CATEGORY_LIST(state, list) {
      state.listOfCategories = list;
    },
    UPDATE_FILTER_CATEGORIES(state, filterCategories){
      state.categoriesToFilter = filterCategories;
    },
    ADD_BID(state, bid) {
      let foundItem = state.listOfItems.find(i => i.item_ID === bid.item_ID);
      foundItem.bids.unshift(bid);
    },
    END_AUCTIONS(state){
      state.isAuctionOpen = false;
      state.listOfItems = [];
      state.listOfCategories = [];
      state.listOfBids = [];
    }
  },
  getters: {
    filteredItems:(state) => {
      if (state.categoriesToFilter.length > 0 && state.listOfItems.length > 0){
        // if an item has a category that matches one in listOfCategories, add it to result
        // find a matching category name in listOfCategories and then add to 
        return state.listOfItems.filter( item => {
          let match = false;
          item.categories.forEach( cat => {
            if( state.categoriesToFilter.some( filterCat => filterCat === cat)){
              match = true;
            }
          });
          return match;
        });
      } else {
       return state.listOfItems;
      }
    },
    listAllTopBids: (state) => {
      //TODO get list of top bids
      const highestBidsByItem = [];
      state.listOfItems.forEach( item => {
        const highestBid = item.bids[item.bids.length - 1];
        highestBidsByItem.unshift( {item_ID: item.item_ID, topBid: highestBid} );
      });
      return highestBidsByItem;
    }
  },
  actions: {
    getAllItems() {
      apiService.getAllItems().then(r => {
        this.commit('SET_ITEM_LIST', r.data);
      }).catch(e => {
        if (e.response) {
          console.error(e.response)
        } else if (e.request) {
          console.error(e.request)
        } else {
          console.error('There was an error!')
        }
      });
    },
    getAllCategories() {
      apiService.getAllCategories().then(r => {
        this.commit('SET_CATEGORY_LIST', r.data);
      }).catch(e => {
        if (e.response) {
          console.error(e.response)
        } else if (e.request) {
          console.error(e.request)
        } else {
          console.error('There was an error!')
        }
      });
    },
    getAllWinnings() {
      apiService.getWinnings().then(r =>{
        this.commit('SET_ITEM_LIST', r.data);      
      }).catch(e => {
        if (e.response) {
          console.error(e.response)
        } else if (e.request) {
          console.error(e.request)
        } else {
          console.error('There was an error!')
        }
      });
    }
  },
})

