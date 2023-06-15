import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import QuillView from '@/views/QuillView.vue'
import MyNotesView from '@/views/MyNotesView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/quill',
      name: 'quill',
      component: QuillView
    },
    {
      path: '/quill/:id',
      name: 'quillId',
      component: QuillView
    },
    {
      path: '/mynotes',
      name: 'mynotes',
      component: MyNotesView
    }
  ]
})

export default router
