import { createRouter, createWebHistory } from 'vue-router'
import HomeViewVue from '@/views/HomeView.vue'
import NoteEditorVue from '@/components/NoteEditor.vue'
import DeletedNotesView from '@/views/DeletedNotesView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeViewVue
    },
    {
      path: '/note-editor/:id?',
      name: 'NodeEditor',
      component: NoteEditorVue,
      props: true
    },
    {
      path: '/deleted-notes',
      name: 'DeletedNotes',
      component: DeletedNotesView
    }
  ]
})

export default router
