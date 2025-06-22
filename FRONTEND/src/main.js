import { createApp } from 'vue'
import App from './App.vue'
import router from './router' // 👈 este es el router que creaste
import './assets/css/style.css';

createApp(App).use(router).mount('#app')



