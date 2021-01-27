import axios from 'axios'

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

export default {
    getAllItems() {
        return axios.get('/items')
      },
    getSingleItem(id) {
        return axios.get(`/items/${id}`);
    },
    getAllCategories() {
        return axios.get('/auction/categories')
    },
    postBid(id, bid) {
        return axios.post(`/items/${id}/bids`, bid);
    },

    getBidsForSingleUser(){
        return axios.get('/user/bids');
    },
    getHighestBidPerItemForSingleUser(){
        return axios.get('/user/bids/current')
    },
    getWinnings() {
        return axios.get('/end-of-auction')
    }

    
}