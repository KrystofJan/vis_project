<script setup>
import { ref, onMounted, watch } from 'vue';
import url from '../config/config.js';

const prop = defineProps(['movie']);

</script>

<template>
<div class="card">
    <h2>{{ movie.movie_name }}</h2>
    <div class="delete-button" @click="$emit('delete-id', prop.movie)">
        <svg xmlns="http://www.w3.org/2000/svg" height="16" width="16" viewBox="0 0 512 512"><path opacity="1" fill="#1E3050" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>
    </div>
</div>

</template>

<style scoped lang="scss">
.card{
    background: white;
    color: #1E3050;
    padding: 2rem;
    width: calc(37% - 9rem);
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-radius: 2rem;
    overflow: hidden;

    h2{
        font-size: .8rem;
        overflow: hidden;
        max-width: 100%;
        text-overflow: ellipsis;
        width: fit-content;
        white-space: nowrap;
    }
    .delete-button{
        cursor: pointer;
        svg{
            display: block;
            width: 3rem;
            height: 3rem;

        path{
            fill: red;
        }
    }
    }

}
</style>
