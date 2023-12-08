<script setup>
import { ref } from "vue";
import url from '../config/config.js';
let input = ref("");
let inputLegacy = ref("");
const apiData = ref([]);

const fetchData = async () => {
    
    try {
        if(input.value != inputLegacy.value){
            inputLegacy.value = input.value
        
            const response = await fetch(url.url + 'actor/search/' + input.value );
            const data = await response.json();
            apiData.value = data;
            console.log(data);
        }
    } catch (error) {
    console.error('Error fetching data:', error);
    }
};

function filteredItems() {
  return apiData.value.filter((item) =>
    item.first_name.toLowerCase().includes(input.value.toLowerCase()) || item.last_name.toLowerCase().includes(input.value.toLowerCase()) 
  );
}
</script>

<template>
    <div class="SearchWrapper">
        <input class="SearchBar" type="text" v-model="input" @input="fetchData" placeholder="Vyhledej herce..." />
        <div class="DropDown-wrapper" v-if="input!=''">
            <div class="DropDown" v-for="item in filteredItems()" :key="item.actor_id">
                <span class="DropDown-item DropDown-name">{{ item.first_name + " " + item.last_name }}</span>
                <span class="DropDown-item DropDown-add" @click="$emit('actor-select', item)">
                    <svg xmlns="http://www.w3.org/2000/svg" height="16" width="16" viewBox="0 0 512 512"><path opacity="1" fill="#1E3050" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM232 344V280H168c-13.3 0-24-10.7-24-24s10.7-24 24-24h64V168c0-13.3 10.7-24 24-24s24 10.7 24 24v64h64c13.3 0 24 10.7 24 24s-10.7 24-24 24H280v64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"/></svg>
                </span>
            </div>
            <div class="DropDown">
                <span class="DropDown-item DropDown-name">Přidat herce</span>
                <router-link to="/add-actor" class="DropDown-item DropDown-add">
                    <svg xmlns="http://www.w3.org/2000/svg" height="16" width="16" viewBox="0 0 512 512"><path opacity="1" fill="#1E3050" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM232 344V280H168c-13.3 0-24-10.7-24-24s10.7-24 24-24h64V168c0-13.3 10.7-24 24-24s24 10.7 24 24v64h64c13.3 0 24 10.7 24 24s-10.7 24-24 24H280v64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"/></svg>
                </router-link>
            </div>
            <div class="DropDown" v-if="input&&!filteredItems().length">
                No results found!        
            </div>
        </div>
    </div>
</template>

<style scoped lang="scss">
.SearchWrapper{
    width: 100%; 
    margin: auto;
    position: relative;

    .SearchBar{
        width: 100%;
        display: block;
        height: 2rem;
        border-radius: 1rem;
        text-align: center;
        position: relative;
        z-index: 2;
    }
    .DropDown-wrapper{
        background: white;
        position: absolute;
        width: calc(100% + 4px);
        padding: calc(1rem + 1px) 0 0 ;
        z-index: 1;
        margin-top: calc(-1rem - 1px);
        border-bottom-right-radius: 2rem;
        border-bottom-left-radius: 2rem;
    }
    .DropDown{
        width: 100%;
        
        color: black;
        display: flex;
        flex-direction: row;
        justify-content: space-between;

        &:not(:last-of-type){
            border-bottom: 1px solid black;
        }

        &-item{
            color: black;
            padding: 1rem 0;
        }

        &-name{
            margin-left: 1rem;
        }
        &-add{
            display: inline-flex;
            justify-content: center;
            align-items: center;
            z-index: 2;
            cursor: pointer;
            margin-right: 1rem;

            svg{
                display: inline-block;
                width: 2rem;
                height: 2rem;

                path{
                    transition: .2s ease-in-out;
                    fill: green;
                }
            }

            &:hover{

                svg{

                    path{
                        fill: lime;
                    }
                }
            }

            
        }
    }
}

</style>
