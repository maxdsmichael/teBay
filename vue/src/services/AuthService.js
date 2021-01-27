import axios from 'axios';

axios.defaults.baseURL = process.env.VUE_APP_REMOTE_API;

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },
}
