import {createRouter, createWebHistory} from 'vue-router';

const routes = [
    {
        path: '/',
        name: 'Home',
        component: () => import('../views/Home.vue')
    },
    {
        path: '/rental',
        name: 'Rental',
        component: () => import('../views/RentalForm.vue')
    },
    {
        path: '/movie',
        name: 'Movie',
        component: () => import('../views/MovieForm.vue')
    },
    {
        path: '/movie-list',
        name: 'MovieList',
        component: () => import('../views/MovieList.vue')
    },
    
    {
        path: '/movie-detail/:id',
        name: 'MovieDetail',
        component: () => import('../views/MovieDetail.vue')
    },
    {
        path: '/add-actor',
        name: 'Přidat herce',
        component: () => import('../views/AddActor.vue')
    }
];

const router = createRouter(
    {
        history: createWebHistory(),
        routes
    }
);
export default router;