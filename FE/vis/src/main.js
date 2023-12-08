import { createApp } from 'vue'
import './style.css'
import router from './router/router';
import App from './App.vue';
import { globalState } from './storage/store';

createApp(App).use(router).provide('globalState', globalState).mount('#app')
