<script setup>
import RentalForm from './views/RentalForm.vue';
import Header from './components/Header.vue';
import { ref, inject } from 'vue';
import { globalState } from './storage/store';


// Access the global state
const prof = ref(globalState.prof);

// Provide the global state to components within the router-view
inject('globalState', globalState);
</script>

<template>
<Header />
<router-view v-slot="{Component}">
  <component :is="Component" :user="prof" />
</router-view>
</template>

<style scoped lang="scss">

</style>
