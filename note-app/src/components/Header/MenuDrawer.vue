<!--
---- Component to 'HeaderAppBar.vue', responsible for the menu drawer UI.
--->

<template>
  <v-navigation-drawer location="right" temporary="">
    <v-card rounded="lg" class="mt-6" elevation="0">
      <v-card-title id="card-title" elevation="6"> <v-icon>mdi-menu</v-icon> Menu </v-card-title>

      <v-list density="compact">
        <br />

        <v-list-item>
          <v-btn to="/" variant="outlined" elevation="24">Home</v-btn>
        </v-list-item>

        <v-list-item>
          <v-btn to="/quill" variant="outlined" elevation="24">New Note</v-btn>
        </v-list-item>

        <v-list-item>
          <v-btn to="/mynotes" variant="outlined" elevation="24">My Notes</v-btn>
        </v-list-item>
      </v-list>
    </v-card>
  </v-navigation-drawer>
</template>
