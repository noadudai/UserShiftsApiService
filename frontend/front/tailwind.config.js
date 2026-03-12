/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'custom-pastel-green' : '#A0C878',
        'custom-cream' : '#FFFDF6',
        'custom-cream-warm' : '#FAF6E9',
        'custom-soft-blue' : '#7794B8',
        'custom-warm-coral-pink' : '#E57B73',
      },
      fontFamily: {
        opensans: ['Open Sans', 'sans-serif'],
      },
    },
  },
  plugins: []
}

