<script setup>
import MovieSearch from '../components/MovieSearch.vue';

import { ref, inject } from 'vue';
const globalState = inject('globalState');

</script>

<template>

<MovieSearch />

</template>

<style scoped lang="scss">

</style>
