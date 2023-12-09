<script setup>
import MovieSearch from '../components/MovieSearch.vue';
import MovieDisplayButton from '../components/MovieDisplayButton.vue';
import url from '../config/config';

import { ref, inject } from 'vue';
const globalState = inject('globalState');
const movieText = ref('');
const apiData = ref([]);

const handleInputEmit = async (input) => {
    try {
        const response = await fetch(url.url + 'Movie/search/' + input.value);
        const data = await response.json();
        apiData.value = data;
        console.log(data);        
    } 
    catch (error) {
    console.error('Error fetching data:', error);
    }
}
</script>

<template>
<div class="Form">
    <div class="FormItem">
        <MovieSearch class="FormItem--movie" :dropdown="false" @emit-input="handleInputEmit"/>
    </div>
</div>
<div class="MovieWrapper">
    <MovieDisplayButton v-for="movie in apiData" :movie="movie" :key="movie.movie_id"/>
</div>
</template>

<style scoped lang="scss">
.block{
    display: block;
}
.MovieWrapper{
    width: 100%;
    display: grid;
    align-self: center;
    grid-template-columns: repeat(auto-fill, minmax(15rem, 1fr));
    gap: 2rem;
}
.Form{
  font-size: 1.5rem;
  display: flex;
  flex-direction: column;
  padding: 5rem;
  gap: 2rem;

  &Item{
    grid-column: 1/-1;
    display: grid;
    grid-template-columns: repeat(3, 1fr);

    &-label{
      grid-column: 1;
      text-align: right;
      transform: translateX(-50%);
    }

    &-item{
      grid-column: 2/-1;
      width: 100%;
      display: block;
      height: 2rem;
      border-radius: 1rem;
    }

    &--movie{
        grid-column: 1/-1;
    }
  }
}
</style>
