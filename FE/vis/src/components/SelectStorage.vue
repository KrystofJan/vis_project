<script setup>
import { onMounted, ref } from "vue";
import url from '../config/config.js';

const storagesFull = defineProps(['storages']);
</script>

<template>
<select class="FormItem-item" name="storages" id="storages">
    <option @click="emit('emit-storage', storage.storage_id)" v-for="storage in storagesFull">
    {{ storage.storage_name }}
    </option>
</select>
</template>

<style scoped lang="scss">

</style>
