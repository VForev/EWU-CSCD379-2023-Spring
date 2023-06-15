<template>
  <v-card>
    <v-card-item>
      <v-table>
        <thead>
          <tr>
            <th class="text-center">Title</th>
            <th class="text-center">Created</th>
            <th class="text-center">Last Modified</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="note in notes" :key="note.noteId">
            <td class="text-center">{{ note.title }}</td>
            <td class="text-center">{{ note.stringCreated }}</td>
            <td class="text-center">{{ note.stringModified }}</td>
            <td class="text-center">
              <v-btn :to="'/quill/' + note.urlSuffix" color="success">Edit</v-btn>
            </td>
            <td class="text-center">
              <v-btn @click="deleteNoteDialog(note)" color="red">Delete</v-btn>
            </td>
          </tr>
        </tbody>
      </v-table>
    </v-card-item>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn @click="previousPage()" variant="outlined">Prev</v-btn>
      <v-btn @click="nextPage()" variant="outlined">Next</v-btn>
      <v-spacer></v-spacer>
    </v-card-actions>
  </v-card>

  <v-dialog
    v-model="removeNoteDialog"
    class="align-center justify-center"
    max-width="400px"
    persistent
  >
    <v-card>
      <v-card-item>
        <v-card-text>Are you sure you want to delete {{ noteToRemove.title }}?</v-card-text>
      </v-card-item>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn @click="submitDeleteNote()" class="bg-error">Yes, I'm sure</v-btn>
        <v-btn @click="removeNoteDialog = !removeNoteDialog" variant="outlined">Cancel</v-btn>
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { Note } from '@/scripts/models/note'
import { inject, ref } from 'vue'
import type { SignInService } from '@/scripts/services/signInService'
import { Services } from '@/scripts/services/services'
import Axios from 'axios'

let id = ''
const signInService = inject(Services.SignInService) as SignInService
const notes = ref<Note[]>([])
let page = 0
let searchNote = ref<string>('')

let removeNoteDialog = ref<boolean>(false)
let noteToRemove = ref<Note>(new Note())

Axios.get('User?username=' + signInService.token.userName + '@noteapp.com')
  .then((response) => (id = response.data))
  .finally(() => {
    Axios.get('Note/GetUsersNotes?appUserId=' + id + '&page=' + page).then((response) => {
      notes.value = response.data as Note[]
      dateTimeFormat(notes.value)
    })
  })

function previousPage() {
  if (page > 0) {
    page--
    Axios.get('Note/GetUsersNotes?appUserId=' + id + '&page=' + page).then((response) => {
      notes.value = response.data as Note[]
      dateTimeFormat(notes.value)
    })
  }
}

function nextPage() {
  if (!(notes.value.length < 10)) {
    page++
    Axios.get('Note/GetUsersNotes?appUserId=' + id + '&page=' + page).then((response) => {
      notes.value = response.data as Note[]
      dateTimeFormat(notes.value)
    })
  }
}

function deleteNoteDialog(note: Note) {
  noteToRemove.value = note
  removeNoteDialog.value = !removeNoteDialog.value
}

function submitDeleteNote() {
  console.log(noteToRemove.value.noteId)
  Axios.post('Note/DeleteNote?noteId=' + noteToRemove.value.noteId).then((response) => {
    console.log(response)
    removeNoteDialog.value = !removeNoteDialog.value
    Axios.get('Note/GetUsersNotes?appUserId=' + id + '&page=' + page).then((response) => {
      notes.value = response.data as Note[]
      dateTimeFormat(notes.value)
    })
  })
}

function dateTimeFormat(notes: Note[]) {
  notes.forEach((note) => {
    note.stringCreated =
      note.created.toString().slice(0, 10) + ', ' + note.created.toString().slice(12, 19)
    note.stringModified =
      note.lastModified.toString().slice(0, 10) + ', ' + note.lastModified.toString().slice(12, 19)
  })
}
</script>
