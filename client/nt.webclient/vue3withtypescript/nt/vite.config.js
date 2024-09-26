import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import { resolve } from 'path'; // Import resolve from 'path'
export default defineConfig({

  plugins: [vue()],
  resolve: {
    alias: {
      '@': resolve(__dirname, 'src'), // Set up alias for src directory
    },
  },
});
