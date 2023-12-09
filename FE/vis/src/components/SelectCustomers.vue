<script setup>
import { onMounted, ref } from "vue";
import url from '../config/config.js';

const customers = defineProps(['customers'])
</script>

<template>
<select class="FormItem-item" name="storages" id="storages">
    <option @click="emit('emit-customer', customer.customer_id)" v-for="customer in customers">
    {{ customer.first_name + " " + customer.last_name }}
    </option>
</select>
</template>

<style scoped lang="scss">

</style>
