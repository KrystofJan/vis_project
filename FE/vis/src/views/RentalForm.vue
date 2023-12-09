<script setup>
import { ref, onMounted, inject } from 'vue';
const globalState = inject('globalState');
import url from '../config/config.js';
// import components
import MovieButton from '../components/MovieButton.vue';
import MovieSearch from '../components/MovieSearch.vue';

// unit of work
const RentalObject = ref({
  movies: [],
  startDate: new Date(),
  endDate: new Date(),
  customer_id: '',
  employee_id: 'e26',
  storage_id: 0
});

// vars
const storages = ref([]);
const customers = ref([]);
const createdId = ref(0);

const Error = ref(false);
const Success = ref(false);

// methods
const fetchStorages = async () => {
    try {
        const response = await fetch(url.url + 'storage/');
        const data = await response.json();
        storages.value = data;
    } catch (error) {
      console.error('Error fetching data:', error);
    }
};

const fetchCustomers = async () => {
    try {
        const response = await fetch(url.url + 'customer/');
        const data = await response.json();
        customers.value = data;
    } catch (error) {
      console.error('Error fetching data:', error);
    }
};

const buildRental = async () => {
  const requestOptions = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json', // Specify the accepted response format
          'Access-Control-Allow-Origin': '*', // Allow requests from any origin (you may need to adjust this based on your server configuration)
          // Add any other headers as needed
        },
        body: JSON.stringify(RentalObject.value)
      };

  fetch(url.url + 'Rental/', requestOptions)
    .then(response => {
        if(response.ok){
          return response.json();
        }
        else{
          return Promise.reject('Request failed');
        }
    } )
    .then(data => {
      console.log('Response:', data);
      createdId.value = data;
      Success.value =true;
    })
    .catch(error => {
        console.error('Error:', error);
        alert('Chyba!');
    });
};

function handleSearchEmit(selectedItemId){
  console.log(selectedItemId);
  RentalObject.value.movies.push(selectedItemId);
  console.log("Got this id: ", RentalObject.value.movies);
}

function handleMovieButtonEmit(id){
  var index = RentalObject.value.movies.indexOf(id);
  console.log(index);
  RentalObject.value.movies.splice(index, 1);
}

const fetchData = async () => {
    await fetchStorages();
    await fetchCustomers();
};

onMounted(fetchData);
</script>

<template>
  <h1>Vytvořit objednávku</h1>
<div v-if="!globalState.prof">
    <div v-if="!Success" class="form">

      <div class="FormItem">
        <label class="FormItem-label" for="storages">Vyberte sklad</label>
        <select class="FormItem-item" name="storages" id="storages">
          <option @click="RentalObject.storage_id=storage.storage_id" v-for="storage in storages">
            {{ storage.storage_name }}
          </option>
        </select>
      </div>

      <div class="FormItem">
        <label class="FormItem-label" for="storages">Vyberte Uživatele</label>
        <select class="FormItem-item" name="storages" id="storages">
          <option @click="RentalObject.customer_id=customer.customer_id" v-for="customer in customers">
            {{ customer.first_name + ' ' + customer.last_name }}
          </option>
        </select>
      </div>

      <div class="FormItem">
        <label class="FormItem-label" for="storages">Vyber film</label>
        <MovieSearch class="FormItem-item" @movie-select="handleSearchEmit" :storage="RentalObject.storage_id" :dropdown="true"/>
      </div>
      <div class="FormItem">
        <label class="FormItem-label" for="storages">Vybrane filmy</label>
        <div class="FormItem-item" id="MovieList">
          <MovieButton  v-for="movie in RentalObject.movies" :movie="movie" @delete-id="handleMovieButtonEmit"/>
        </div>
      </div>
      <div class="FormItem">
        <label class="FormItem-label" for="date-start">Start date:</label>
        <input  v-model="RentalObject.startDate" class="FormItem-item" type="date" id="date-start" name="date-start"/>
      </div>
      <div class="FormItem">
        <label class="FormItem-label" for="date-end">End date:</label>
        <input v-model="RentalObject.endDate" class="FormItem-item" type="date" id="date-end" name="date-end"/>
      </div>
      
      <div class="FormItem">
        <div @click="buildRental" class="submit-button">
          Vytvoř objednávku
        </div>
      </div>
      
    </div>
    <div v-if="Success" class="success">
      Objednávka {{ createdId }} 
    </div>
</div>
<div v-else>
  Musite byt prihlaseny jako zamestnanec
</div>
</template>

<style scoped lang="scss">
h1{
  margin: 1.5rem 0 0 3rem;
}
.form{
  font-size: 1.5rem;
  display: flex;
  flex-direction: column;
  padding: 5rem;
  gap: 2rem;

  .FormItem{
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
  }
}
#MovieList{
  gap: 3rem;
  display: flex;
  flex-flow: row wrap;
  height: auto;
  justify-content: center;
  gap: 3rem;
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
</style>
