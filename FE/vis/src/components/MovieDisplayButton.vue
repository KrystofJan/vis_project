<script setup>
import { ref, onMounted, watch } from 'vue';
import url from '../config/config.js';

const prop = defineProps(['movie']);
const my_url = ref('');

onMounted( () => {
    my_url.value = "/movie-detail/" + prop.movie.movie_id;
} );
</script>

<template>
<div class="Card">
    <router-link class="CardLink" :to="my_url">
        <div class="CardImg">
            <img :src="prop.movie.picture_path" alt="temp">
        </div>
        <div class="Card-title">{{ prop.movie.movie_name }}</div>
    </router-link>
    </div>


</template>

<style scoped lang="scss">
.Card{
    width: 15rem;
    height: 15rem;
    position: relative;
    display: flex;
    justify-content: center;
    border: 0.2rem solid black;
    border-radius: 2rem;
    align-items: flex-end;
    overflow: hidden;
    transition: .2s ease-in-out;
    cursor: pointer;

    &Link{
        width: 100%;
        display: inline-block;
    }

    &:hover{
        scale: 1.1;
    }

    &Img{
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        overflow: hidden;
        z-index: -1;
        
        img{
            width: 100%;
            object-fit: cover;
            object-position: center;
        }

    }
    &-title{
        font-weight: 600;
        display: block;
        font-size: 2rem;
        width: 100%;
        background: white;
        color: black;
        text-align: center;
        text-overflow: ellipsis;
        max-height: 6rem;
    }
}
</style>
