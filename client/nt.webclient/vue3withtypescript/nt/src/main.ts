import { createApp, h } from 'vue';
import App from './App.vue';
import router from './router';
import { createPinia } from 'pinia';
import { provideApolloClient } from '@vue/apollo-composable';
import apolloClient from './apolloClient';

const pinia = createPinia();
const app = createApp({
  setup() {
    provideApolloClient(apolloClient);
  },

  render: () => h(App),
});

app.use(pinia).use(router).mount('#app');
