<template>
  <v-container
    ><v-card
      ><v-card-title style="display: flex; justify-content: space-between"
        ><div>Notes:</div>
        <div style="display: flex; gap: 10">
          <v-text-field
            style="width: 200px"
            density="compact"
            variant="solo"
            label="search note..."
            append-inner-icon="mdi-magnify"
            single-line
            hide-details
            @input="SetSearchParams($event.target.value)"
          ></v-text-field
          ><v-btn to="/note-editor">Create Note</v-btn>
        </div></v-card-title
      ><v-list v-for="note in notes.filter((e) => e.title.startsWith(searchParams))" :key="note.id"
        ><v-list-item rounded="xl" class="mx-auto" @click="void 0">
          <div style="display: flex; justify-content: space-between; align-items: center">
            {{ note.title }}
            <div style="display: flex; align-items: center; gap: 20px">
              <div>Date Deleted: {{ note.deletedDate?.split('T', 1).toString() }}</div>
              <v-hover>
                <v-tooltip text="Restore">
                  <template v-slot:activator="{ props }">
                    <v-icon
                      v-bind="props"
                      icon="mdi-delete-restore"
                      @click="RestoreNote($event, note.id)"
                    />
                  </template>
                </v-tooltip>
              </v-hover>
            </div>
          </div> </v-list-item></v-list></v-card
  ></v-container>
</template>

<script setup lang="ts">
import Axios from 'axios'
import { ref } from 'vue'

interface INote {
  id: string
  title: string
  content: string
  created: string
  deletedDate?: string
}

const notes = ref<INote[]>([])
const searchParams = ref<string>('')

const SetSearchParams = (text: string) => {
  searchParams.value = text
}

const RestoreNote = async (e: any, id: string) => {
  e.preventDefault()
  await Axios.put(`/Note/RestoreNote?id=${id}`)
  await GetDeletedNotes()
}
const GetDeletedNotes = () => {
  Axios.get('/Note/GetDeletedNotes').then((result) => {
    notes.value = result.data as INote[]
  })
}

GetDeletedNotes()
</script>
