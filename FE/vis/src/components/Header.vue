<script setup>
import { ref } from 'vue';
const userType = ref(false);
const ShowDrop = ref(false);

const emit = defineEmits();

import { globalState } from '../storage/store';

const changeProfile = () => {
    console.log(globalState.prof);
  globalState.prof = !globalState.prof;
};

</script>

<template>
    <div class="Header">
        <div class="Header-left Header-Part">
            <router-link to="/" class="Header-Part-item">Dionysos</router-link>
        </div>
        <nav class="Header-right Header-Part">
            <router-link v-if="!globalState.prof" to="/rental" class="Header-Part-item">Rental</router-link>
            <router-link v-if="!globalState.prof" to="/movie" class="Header-Part-item">Movie</router-link>
            <router-link to="/movie-list" class="Header-Part-item">MovieList</router-link>
            <router-link to="/" class="Header-Part-item">Esketit</router-link>
            <svg class="usr" @click="ShowDrop = !ShowDrop" xmlns="http://www.w3.org/2000/svg" height="16" width="16" viewBox="0 0 512 512"><path opacity="1" fill="#1E3050" d="M399 384.2C376.9 345.8 335.4 320 288 320H224c-47.4 0-88.9 25.8-111 64.2c35.2 39.2 86.2 63.8 143 63.8s107.8-24.7 143-63.8zM0 256a256 256 0 1 1 512 0A256 256 0 1 1 0 256zm256 16a72 72 0 1 0 0-144 72 72 0 1 0 0 144z"/></svg>
            <div class="drop" v-if="ShowDrop">
                <label class="switch">
                    <input type="checkbox">
                    <span class="slider round" @click="changeProfile">
                        <span class="bef">
                            {{ (globalState.prof)? "C" : "E" }}
                        </span>
                    </span>
                </label>
            </div>
        </nav>
    </div>
</template>

<style scoped lang="scss">
    .Header{
        display: block;
        width: 100%;
        position: sticky;
        z-index: 999;
        top:0;
        left: 0;
        min-height: 5rem;
        display: flex;
        justify-content: space-between;
        background-color: #1c1b22;
        align-items: center;
        
        &-Part{
            margin: 1rem 3rem;

            &-item{
                text-decoration: none;
                color: white;
            }
        }
        &-right{
            width: 40%;
            display: flex;
            justify-content: space-around;
        }
    }

     /* The switch - the box around the slider */
.switch {
  position: relative;
  display: inline-block;
  width: 60px;
  height: 34px;
}

/* Hide default HTML checkbox */
.switch input {
  opacity: 0;
  width: 0;
  height: 0;
}

/* The slider */
.slider {
  position: absolute;
  cursor: pointer;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: #ccc;
  -webkit-transition: .4s;
  transition: .4s;
}

.bef {
  position: absolute;
  content: "";
  height: 26px;
  width: 26px;
  left: 4px;
  bottom: 4px;
  background-color: white;
  -webkit-transition: .4s;
  transition: .4s;
}

input:checked + .slider {
  background-color: #2196F3;
}


input:focus + .slider {
  box-shadow: 0 0 1px #2196F3;
}

input:checked + .slider .bef {
  -webkit-transform: translateX(26px);
  -ms-transform: translateX(26px);
  transform: translateX(26px);
}

/* Rounded sliders */
.slider.round {
  border-radius: 34px;
}
.bef{
    color: black;
    display: flex;
    justify-content: center;
    align-items: center;
}

.slider.round .bef {
  border-radius: 50%;
} 
.usr{
    display: block;
    height: 1.5rem;
    width: 1.5rem;
    cursor: pointer;

    path{
        fill: white;
    }
}
.drop{
    position: absolute;
    right: 0;
    top: 5rem;
    background: #1c1b22;
    border: 1px solid #1c1b22;
    display: flex;
    justify-content: center;
    align-items: center;
    border-bottom-right-radius: 2rem;
    border-bottom-left-radius: 2rem;
    width: 13rem;
    height: 5rem;
}

.blue{
    path{
        fill: #2196F3;
    }
}
</style>
