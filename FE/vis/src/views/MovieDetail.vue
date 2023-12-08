<script setup>
import { ref, onMounted  } from 'vue';
import { useRoute } from 'vue-router'
import url from '../config/config';

const movie = ref({});
const storages = ref([]);
const Allstorages = ref([]);
const route = useRoute();

const storagesEdited = ref([]);


const filteredStorages = ref([]);
const input = ref('');


const getData = async () => {
    try {
        const response = await fetch(url.url + 'Movie/' + route.params.id);
        const data = await response.json();
        movie.value = data;

        const response2 = await fetch(url.url + 'Stock/Movie/' + movie.value.movie_id);
        const stocks = await response2.json();

        const response3 =  await fetch(url.url + 'storage/');
        Allstorages.value = await response3.json();

        for(const stock of stocks){
            const response4 = await fetch(url.url + 'Storage/' + stock.storage.storage_id);
            const st = await response4.json();
            storages.value.push(st);
        }

        for(const stock of Allstorages.value){
            let is_inS = false;
            for(const stor of storages.value){
                if (stock.storage_id == stor.storage_id){
                    is_inS = true;
                    break;
                }
            }
            storagesEdited.value.push({
                address: stock.address,
                storage_name: stock.storage_name,
                storage_id: stock.storage_id,
                isIn: is_inS
            });
        }
    } 
    catch (error) {
        console.error('Error fetching data:', error);
    }
}

const fetchStorage = async () => {
    try {
        filteredStorages.value = [];
        const response = await fetch(url.url + 'search/' + input.value);
        const data = await response.json();
    
        for(const stock of data){
            let is_inS = false;
            for(const stor of storages.value){
            console.log(stor);

                if (stock.storage_id == stor.storage_id){
                    is_inS = true;
                    break;
                }
            }
            filteredStorages.value.push({
                address: stock.address,
                storage_name: stock.storage_name,
                storage_id: stock.storage_id,
                isIn: is_inS
            });
        
        }
    }
    catch (error) {
        console.error('Error fetching data:', error);
    }
}

onMounted(
    getData
);
</script>

<template>

<div class="MovieDetail">
    <div class="MovieDetail-item Picture">
       <!-- <img :src="movie.picture_path" alt="tmp"> -->
        <img src="../assets/vue.svg" alt="">
    </div>

    <h2 class="MovieDetail-item Name">
        {{ movie.movie_name }}
    </h2>
    <div class="MovieDetail-item ActorList">
        <span class="ActorList" v-for="actor in movie.actors">
            {{ actor.first_name + " " + actor.last_name + ", " }}
        </span>
    </div>
    <div class="Popis">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Iste natus veritatis sint sequi perferendis pariatur quia quod, voluptate nemo neque quibusdam reiciendis fugit fugiat mollitia minima totam placeat recusandae saepe?
        Vero fuga eveniet enim minus minima culpa quo aspernatur omnis debitis cum tempora reiciendis eum deleniti, nihil doloribus consectetur rerum corporis velit voluptatem aliquid assumenda, sapiente ratione amet! Hic, vitae.
        Iure, ad, et qui alias itaque accusantium quos, vero architecto dignissimos est ipsa a autem! Reprehenderit, ipsa similique? Voluptate illo beatae aliquid, porro eius est. Laboriosam dolore maxime eveniet et?
        Consectetur qui veniam, saepe maxime laborum aut illo magnam. Libero, magni maxime quas neque deleniti repellat quibusdam odio, fugit beatae distinctio rem ipsa quo ab consequuntur dolor eligendi quasi omnis.
    </div>
</div>
<div class="storageHeader">
    <h3>Dostupnost</h3>
    <input v-model="input" @input="fetchStorage" type="text" class="StorageSearch" placeholder="Vyhledejte pobočku...">
</div>
<table class="Storages">
  <tr>
    <th>Adresa půjčovny</th>
    <th>Název obchodu</th>
    <th>Dostupne</th>
  </tr>
  <tr v-if="input == '' " v-for="storage in storagesEdited">
    <td>{{ storage.address.city + ", " + storage.address.street + " "+ storage.address.building_number}}</td>
    <td>{{ storage.storage_name}}</td>
    <td v-if="!storage.isIn">
        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" viewBox="0 0 512 512"><path opacity="1" fill="red" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>
    </td>
    <td v-else>
        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" viewBox="0 0 512 512"><path opacity="1" fill="green" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM232 344V280H168c-13.3 0-24-10.7-24-24s10.7-24 24-24h64V168c0-13.3 10.7-24 24-24s24 10.7 24 24v64h64c13.3 0 24 10.7 24 24s-10.7 24-24 24H280v64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"/></svg>
    </td>
  </tr>
  <tr v-if="input != '' " v-for="storage in filteredStorages">
    <td>{{ storage.address.city + ", " + storage.address.street + " "+ storage.address.building_number}}</td>
    <td>{{ storage.storage_name}}</td>
    <td v-if="!storage.isIn">
        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" viewBox="0 0 512 512"><path opacity="1" fill="red" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM175 175c9.4-9.4 24.6-9.4 33.9 0l47 47 47-47c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9l-47 47 47 47c9.4 9.4 9.4 24.6 0 33.9s-24.6 9.4-33.9 0l-47-47-47 47c-9.4 9.4-24.6 9.4-33.9 0s-9.4-24.6 0-33.9l47-47-47-47c-9.4-9.4-9.4-24.6 0-33.9z"/></svg>
    </td>
    <td v-else>
        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24" viewBox="0 0 512 512"><path opacity="1" fill="green" d="M256 512A256 256 0 1 0 256 0a256 256 0 1 0 0 512zM232 344V280H168c-13.3 0-24-10.7-24-24s10.7-24 24-24h64V168c0-13.3 10.7-24 24-24s24 10.7 24 24v64h64c13.3 0 24 10.7 24 24s-10.7 24-24 24H280v64c0 13.3-10.7 24-24 24s-24-10.7-24-24z"/></svg>
    </td>
  </tr>
</table>

</template>

<style scoped lang="scss">

.storageHeader{
    text-align:center;
    margin: 2rem 0;
    width: 100%;
    .StorageSearch{
        display: block;
        height: 2rem;
        width: 75%;
        margin: auto;
        border-radius: 1rem;
        text-align: center;
        position: relative;
        z-index: 2;
    }
        
}
.MovieDetail{
    padding: 4rem;
    display: grid;
    grid-template-areas: 
        "pic pic name"
        "pic pic names"
        "pic pic popis";
    gap: 2rem;

    .Picture{
        grid-area: pic;
        width: 36rem;
        padding: 2rem;

        img{
            width: 100%;
            height: 100%;
            object-fit: cover;
        }
    }
    .Name{
        grid-area: name;
    }
    .ActorList{
        grid-area: names;
    }
    .Popis{
        grid-area: popis;
    }
}

.Storages {
    font-family: Arial, Helvetica, sans-serif;
    border-collapse: collapse;
    margin: auto;
    width: 75%;

    tr{

        td, th{
            border: 1px solid #ddd;
            padding: 8px;
        }

        th {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #082f47;
            color: white;
        }

        &:nth-child(even){
            background-color: black;
        }
        
        &:hover {
            background-color: #333;
        }

    }

}

</style>
