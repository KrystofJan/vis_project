<script setup>
import { ref, inject } from 'vue';
import url from '../config/config.js';

const globalState = inject('globalState');
const createdId = ref(0);
const Success = ref(false);

const actor = ref({
    first_name: '',
    last_name: ''
})


const buildActor = async () => {
  console.log(actor.value);
  const requestOptions = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json',
          'Access-Control-Allow-Origin': '*'
        },
        body: JSON.stringify(actor.value)
      };

  fetch(url.url + 'actor/', requestOptions)
    .then(response => {
        if(response.ok){
          return response.json();
        }
        else{
          alert("Chyba!");
          return response.json().then(error => Promise.reject(error));
        }
    } )
    .then(data => {
      console.log('Response:', data);
      createdId.value = data;
      Success.value = true;
    })
    .catch(error => {
        console.error('Error:', error);
        alert(error);
    });
};
</script>

<template>
  <h1>Vytvořit herce</h1>
<div v-if="!globalState.prof"> 
  <div class="Form-wrap" v-if="!Success">
    <div class="Form">
        <div class="FormItem">
            <label class="FormItem-label" for="first-name">Křestní jméno</label>
            <input v-model="actor.first_name" class="FormItem-item" type="text" id="first-name">
        </div>
        <div class="FormItem">
            <label class="FormItem-label" for="last-name">Příjmení</label>
            <input v-model="actor.last_name" class="FormItem-item" type="text" id="last-name">
        </div>
    </div>

    <div class="FormItem">
        <div @click="buildActor" class="submit-button">
          Vytvoř objednávku
        </div>
      </div></div>
      <div class="success" v-else>
        Herec byl uspěšně vytvořen s id {{ createdId }}
      </div>
</div>

<div v-else>
    Musite byt prihlasen jako zamestnanec
</div>

</template>

<style scoped lang="scss">
h1{
  margin: 1.5rem 0 0 3rem;
}
.success{
    background: greenyellow;
  color: darkgreen;
  text-align: center;
  width: fit-content;
  margin: auto;
  padding: 2rem;
  border-radius: 2rem;
}

.Form{
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
