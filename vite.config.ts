import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  root: 'ExpenseTracker/src',
  build: {
    outDir: '../../ExpenseTracker/wwwroot',
    emptyOutDir: true,
  },
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7240',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
