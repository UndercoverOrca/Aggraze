<script setup lang="ts">
import { ref, onMounted } from 'vue';

const artists = ref([]);

onMounted(async () => {
  const response = await fetch('/test/artists');
  artists.value = await response.json();
})

</script>

<template>
  <h1>Artists</h1>
  <ul>
    <li v-for="artist in artists" :key="artist">
      {{ artist }}
    </li>
  </ul>
</template>

<style scoped></style>
