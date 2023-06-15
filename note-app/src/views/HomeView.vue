<template>
  <!-- TODO: Change min width of v-responsive. -->
  <v-responsive min-width="300px">
    <v-card>
      <v-card-title class="justify-center">Welcome To Note App!</v-card-title>
      <v-card-item>
        <v-card-text> </v-card-text>
        <v-card-actions class="justify-center">
          <v-btn to="/quill" variant="tonal" rounded="xl" elevation="6">Create New Note</v-btn>
          <v-btn to="/mynotes" variant="outlined" rounded="xl" elevation="6">My Notes</v-btn>
        </v-card-actions>
      </v-card-item>
    </v-card>
  </v-responsive>
</template>
