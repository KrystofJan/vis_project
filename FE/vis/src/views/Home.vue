<script setup>
import { ref, inject } from "vue";
// import { globalState } from '../storage/store';

const globalState = inject('globalState');

</script>

<template>
DiosMio
{{ globalState.prof }}

</template>

<style scoped lang="scss">
</style>
