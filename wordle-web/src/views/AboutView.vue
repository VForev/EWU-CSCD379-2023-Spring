<template>
  <v-sheet color="blue" height="170px" class="pt-10 my-5">
    <v-text-field>
      This project its sooper cool and I love exploring the tools that are avalibe to us. This is a
      great learning exprice but wish I had more time between assigments
    </v-text-field>
  </v-sheet>

  {{ isDialogOpen }}
  <WeatherDialog v-model="isDialogOpen" :weather="currentWeather!"></WeatherDialog>

  <v-card v-for="item in weatherData" :key="item.date" @click="setCurrentWeather(item)">
    {{ item.date }}</v-card
  >
</template>

<script setup lang="ts">
import Axios from 'axios'
import { ref } from 'vue'
import WeatherDialog from '@/components/WeatherDialog.vue'
import type { WeatherData } from '@/types/WeatherData'

const isDialogOpen = ref(false)
const weatherData = ref<WeatherData[]>()
const currentWeather = ref<WeatherData>()

function setCurrentWeather(weather: WeatherData) {
  currentWeather.value = weather
  isDialogOpen.value = true
}

Axios.get('https://localhost:7053/WeatherForecast')
  .then((response) => {
    console.log(response.data)
    weatherData.value = response.data
  })
  .catch((err) => {
    console.log(err)
  })
</script>

<style>
@media (min-width: 1024px) {
  .about {
    min-height: 100vh;
    display: flex;
    align-items: center;
  }
}
</style>
-->

<!-- ----------------------------the script has broken code but needed for inclass stuff --------KEEP-------------------- -->

<!-- <script setup lang="tx">
import Axios from "axios"

Axios.get("https://localhost:7255/WeatherForecast")
  .then((res) => {
    console.log(res)
  })
  .catch((err) => {
    console.log(err)
  })
</script> -->

<!-- ----------------------------the script has broken code but needed for inclass stuff --------KEEP-------------------- -->
