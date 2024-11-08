import { createApp } from 'vue';
import App from './App.vue';

import VueTheMask from 'vue-the-mask';

import { v4 as uuidv4 } from 'uuid'; // імпортуємо uuidv4

const app = createApp(App);

// Реєструємо директиву v-mask
app.use(VueTheMask);

console.log(VueTheMask);

// Реєструємо uuidv4, якщо потрібно в інших компонентах
app.use('uuidv4', uuidv4);

app.mount('#app');
