import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import vueDevTools from 'vite-plugin-vue-devtools'
import { resolve } from 'path'; // Import resolve from 'path'
export default defineConfig({

  plugins: [vue(), vueDevTools()],
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src'), // Set up alias for src directory
    },
  },
});
