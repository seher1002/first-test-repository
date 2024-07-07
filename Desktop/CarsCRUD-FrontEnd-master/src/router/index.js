import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import CarsView from '../views/Cars/CarsView.vue'
import EditCarsView from '../views/Cars/EditCarsView.vue'
import AddCarsView from '../views/Cars/AddCarsView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('../views/AboutView.vue')
      },
      {
          path: '/Cars',
         name: 'Cars',
         component: CarsView
      },
      {
          path: '/EditCars',
          name: 'EditCars',
          component: EditCarsView
      },
      {
          path: '/AddCars',
          name: 'AddCars',
          component: AddCarsView
      },
  ]
})

export default router
