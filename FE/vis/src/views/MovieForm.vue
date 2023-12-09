<script setup>
import { ref, inject, onMounted } from 'vue';
import url from '../config/config.js';
import ActorSearch from '../components/ActorSearch.vue';
import ActorButton from '../components/ActorButton.vue';

const globalState = inject('globalState');
const movie = ref({
    actors: [],
    movie_name: '',
    price_per_day: 0.0,
    picture_path: ''
});

const createdId = ref(0);
const filePath = ref('');
const Success = ref(false);
const storageId = ref(0);
const ammount = ref(0);
const storages = ref([]);

function file_path(event) {
    console.log(event.target.value);
    movie.picture_path = event.target.value;
}

function handleAddActors(actor){
    movie.value.actors.push(actor);
}

function handleDeleteActorEmit(id){
    const index = movie.value.actors.indexOf(id);
    movie.value.actors.splice(index, 1);
}

const fetchStorages = async () => {
    try {
        const response = await fetch(url.url + 'storage/');
        const data = await response.json();
        storages.value = data;
    } catch (error) {
      console.error('Error fetching data:', error);
    }
};

const addStock = async () => {
    const requestOptions = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Access-Control-Allow-Origin': '*'
        },
        body: JSON.stringify({
            movie_id: createdId.value,
            storage_id: storageId.value,
            ammount: document.querySelector('#movieNumber').value
        })
      };

      return new Promise((resolve, reject) => {
        fetch(url.url + 'stock/', requestOptions)
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    reject('Špatná data');
                }
            })
            .then(data => {
                console.log('Response:', data);
                // createdId.value = data;
                Success.value = true;
                resolve(data);
            })
            .catch(error => {
                console.error('Error:', error);
                console.log({
                    movie_id: createdId.value,
                    storage_id: storageId.value,
                    ammount: document.querySelector('#movieNumber').value
                });
                Success.value = false;
                alert('Nastala chyba při vytváření záznamu o kopii na skladě!', error);
                reject(error);
            });
    });
}
const buildMovie = async () => {
  const requestOptions = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Access-Control-Allow-Origin': '*'
        },
        body: JSON.stringify(movie.value)
      };

      return new Promise((resolve, reject) => {
        fetch(url.url + 'movie/', requestOptions)
            .then(response => {
                if (response.ok) {
                    return response.json();
                } else {
                    alert(response.json().value);
                    Success.value = false;

                    reject("Špatná");
                }
            })
            .then(data => {
                console.log('Response:', data);
                createdId.value = data;
                console.log(createdId.value);
                resolve(data);
            })
            .catch(error => {
                console.error('Error:', error);
                Success.value = false;
                alert('Nastala chyba při vytváření filmu!', error);
                reject(error);
            });
    });
};

const build = async () => {
    try {
        await buildMovie();
        await addStock();
    } catch (error) {
        // Handle errors here
        console.error('Error:', error);
    }
}

onMounted(fetchStorages);
</script>

<template>
    <div v-if="!globalState.prof"> 
<div v-if="!Success"  class="Form">
    <div class="FormItem movie">
        <label for="movieName">Název filmu</label>
        <input class="FormItem-item" type="text" id="movieName" v-model="movie.movie_name">
    </div>
    <div class="FormItem price">
        <label for="movieNumber">Cena za den</label>
        <input step=".01" class="FormItem-item" type="number" id="moviePrice" v-model="movie.price_per_day">
    </div>
    <div class="FormItem storage">
        <label class="FormItem-label" for="storages">Vyberte sklad</label>
        <select class="FormItem-item" name="storages" id="storages">
          <option @click="storageId=storage.storage_id" v-for="storage in storages">
            {{ storage.storage_name }}
          </option>
        </select>
    </div>
    <div class="FormItem number">
        <label for="movieNumber">Počet kusů</label>
        <input 
            class="FormItem-item" 
            type="number" 
            id="movieNumber">
    </div>
    <div class="FormItem image">
        <input 
            class="FormItem-item" 
            type="file" 
            id="movieImage" 
            value="Vyber obrazek">
    </div>
    <div class="FormItem actorSearch">
        <ActorSearch 
            @actor-select="handleAddActors" 
            />
    </div>
    <div class="FormItem actors">
      <label class="FormItem-label" for="storages">Vybrane filmy</label>
      <div class="FormItem-item" id="MovieList">
        <ActorButton  
            v-for="a in movie.actors"
            :actor="a"
            @delete-id="handleDeleteActorEmit"
         />

      </div>
    </div>
    <div class="FormItem submit">
      <div @click="build" class="submit-button">
        Vytvoř objednávku
      </div>
    </div>
</div>
<div v-if="Success">
Uspesne vytvereny film {{ createdId }}</div>
</div>
<div v-else>
    Musite byt prihlasen jako zamestnanec
</div>

</template>

<style scoped lang="scss">
.Form{
    display: grid;
    grid-template-areas:
            "name picture picture"
            "price picture picture"
            "storage picture picture"
            "number picture picture"
            "search search search"
            "actor actor actor"
            ". submit .";
    padding: 5rem;
    gap: 2rem;

  &Item{
    display: grid;
    grid-template-rows: 3rem 1fr;

    &-item{
      width: 100%;
      display: block;
      height: 2rem;
      border-radius: 1rem;
    }
  }
    .movie{
        grid-area: name;
    }
    .image{
        grid-area: picture;
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .price{
        grid-area: price;
    }
    .actorSearch{
        grid-area: search;
    }
    .actors{
        grid-area: actor;
    }
    .submit{
        grid-area: submit;
        display: block;
    }
    #MovieList{
        display: flex;
        flex-flow: row wrap;
        justify-content: center; 
        gap: 2rem;
        height: auto;
    }

    .submit-button{
        padding: 1rem;
        background: orange;
        grid-column: 2;
        text-align: center;
        color: black; 
        cursor: pointer;
        transition: .2s ease-in-out;

        &:hover{
            background: lightsalmon;
        }
    }
}
#movieImage{
    display: flex;
    justify-content: center;
    align-items: center;
    width: 12rem;
    height: 12rem;
    background: #002230;
    border: 1px solid #2f4161;
    
}
</style>
