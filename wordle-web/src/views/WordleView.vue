<template>
  <WordleGame :isWordOfTheDay="true" :wordOfTheDayDate="date"></WordleGame>
</template>

<script setup lang="ts">
import WordleGame from '@/components/WordleGame.vue'
</script>
