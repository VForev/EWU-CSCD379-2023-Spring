<template>
  <v-container
    ><v-row justify="center">
      <v-col cols="12" style="display: flex; justify-content: center">
        <div style="display: flex; align-items: center; justify-content: center; flex-wrap: wrap">
          <h1 class="text-h1">Noted</h1>
        </div>
      </v-col>
      <v-col cols="12" style="display: flex; justify-content: center">
        <v-card
          style="
            padding: 10px;
            display: flex;
            flex-direction: row;
            height: 800px;
            width: fit-content;
          "
        >
          <div>
            <v-text-field
              :model-value="currentNote?.title"
              @input="editNoteTitle($event.target.value)"
              variant="underlined"
              placeholder="insert title..."
              :error-messages="emptyTitleError ? 'Note must contain title' : ''"
            ></v-text-field>
            <v-textarea
              :model-value="currentNote?.content"
              @input="editNoteContent($event.target.value)"
              variant="outlined"
              style="display: flex; flex-direction: column; flex-grow: 1; width: 500px"
            ></v-textarea>

            <div style="display: flex; justify-content: right; gap: 10px">
              <v-btn @click="deleteNote()" to="/">
                <div style="display: flex; align-items: center; gap: 10px">
                  <v-icon icon="mdi-delete" />
                  <div>Delete</div>
                </div>
              </v-btn>
              <v-btn @click="handleSave()">
                <div style="display: flex; align-items: center; gap: 10px">
                  <v-icon icon="mdi-content-save-outline" />
                  <div>Save</div>
                </div>
              </v-btn>
            </div>
          </div>
        </v-card>
      </v-col>
    </v-row></v-container
  >
</template>

<script setup lang="ts">
import router from '@/router'
import Axios from 'axios'
import { ref } from 'vue'
import { useRoute } from 'vue-router'

interface INote {
  id?: string
  title: string
  content: string
  created: string
  deletedDate?: string
}

const route = useRoute()
const id = route.path.split('/')[2]
const emptyTitleError = ref<boolean>(false)
// dsfd

if (id) {
  Axios.get(`/Note/GetNoteById?id=${id}`).then((result) => {
    currentNote.value = result.data
  })
}

const currentNote = ref<INote>({ title: '', content: '', created: '' })

const editNoteTitle = (text: string) => {
  currentNote.value.title = text
}

const editNoteContent = (text: string) => {
  currentNote.value.content = text
}

const deleteNote = () => {
  console.log(currentNote.value.id)
  currentNote.value.id && Axios.delete(`/Note/Delete?id=${currentNote.value.id}`)
}

const createNote = () => {
  console.log(currentNote.value)
  Axios.post('/Note/Add', currentNote.value)
}

const updateNote = () => {
  Axios.put('/Note/Update', currentNote.value)
}

const handleSave = () => {
  if (currentNote.value.title !== '') {
    currentNote.value.id ? updateNote() : createNote()
    router.push('/')
  } else {
    emptyTitleError.value = true
  }
}
</script>
