<script setup>
import { ref, inject } from "vue";
// import { globalState } from '../storage/store';

const globalState = inject('globalState');

</script>

<template>
    <div class="HP">
        <h1>Půjčovna filmů dionýsos</h1>
        <div class="image">
            <img src="../assets/logo/logo.png" alt="logo" />
        </div>
        <p>Půjčte si film na jakékoliv z našich poboček</p>
    </div>
</template>

<style scoped lang="scss">
.HP{
    display: flex;
    flex-flow: column;
    width: 100%;
    justify-content: center;
    align-items: center;
    h1{
        font-size: 2rem;
    }
    .image{
        width: 24rem;
        height: 24rem;

        img{
            width: 100%;
            height: 100%;
        }
    }
}
</style>
