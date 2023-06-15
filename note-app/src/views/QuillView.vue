<template>
  <!--<v-card style="max-width: 1280px;" id="quillEditor">-->
  <div>
    <v-text-field v-model="title" label="Title"></v-text-field>
  </div>
  <v-card style="height: 80%; max-width: 1280px" id="quillEditor" elevation="4">
    <QuillEditor v-model:content="text" content-type="html" v-bind="options" />
  </v-card>
  <br />
  <div class="text-center">
    <v-btn @click="saveNote()" elevation="4">Save</v-btn>
  </div>
  <v-dialog v-model="noteSavedDialog" max-width="400px" persistent>
    <v-card>
      <v-card-title class="text-center">Your note was saved!</v-card-title>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn @click="noteSavedDialog = !noteSavedDialog" :to="'/quill/' + note.urlSuffix"
          >Ok</v-btn
        >
        <v-spacer></v-spacer>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
import { QuillEditor } from '@vueup/vue-quill'
import '@vueup/vue-quill/dist/vue-quill.snow.css'
import { inject, ref } from 'vue'
import { Note } from '@/scripts/models/note'
import { SignInService } from '@/scripts/services/signInService'
import { Services } from '@/scripts/services/services'
import Axios from 'axios'
import router from '@/router'

const signInService = inject(Services.SignInService) as SignInService

let id = ''
let text = ref<string>('')
let title = ref<string>('')
let note = ref<Note>(new Note())
let noteSavedDialog = ref<boolean>(false)

// TODO: Modify so we don't hardcode the email address...
Axios.get('User?username=' + signInService.token.userName + '@noteapp.com').then(
  (response) => (id = response.data)
)

const options = {
  toolbar: 'full',
  theme: 'snow'
}

if (router.currentRoute.value.params.id != null) {
  Axios.get('Note/ByUrlSuffix?urlSuffix=' + router.currentRoute.value.params.id).then(
    (response) => {
      note.value = response.data as Note
      title.value = note.value.title
      text.value = note.value.content
    }
  )
}

function saveNote() {
  if (title.value == null || title.value == '') {
    title.value = 'Untitled'
  }

  note.value.title = title.value
  note.value.content = text.value
  note.value.appUserID = id
  // TODO: Modify console log of response...
  Axios.post('/Note/CreateOrEditNote', note.value).then((response) => {
    console.log(response)
    note.value = response.data as Note
    noteSavedDialog.value = !noteSavedDialog.value
  })
}
</script>
