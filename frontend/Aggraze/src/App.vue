<script lang="ts">
  type Artist = {
    name: string;
    genre: string;
  };
</script>

<script setup lang="ts">
  import { ref } from 'vue';

  const artists = ref<Artist[]>([]);
  const showArtists = ref(true);

  async function getArtists() {
    artists.value = await (await fetch('/test/artists')).json();
  }
</script>

<template>
  <h1>Artists</h1>
  <ul v-if="artists.length && showArtists">
    <li v-for="artist in artists" :key="artist.name">
      {{ artist.genre }} artist {{ artist.name }}
    </li>
  </ul>
  <button type="button" v-if="artists.length === 0" @click="getArtists()">
    Show me the artists
  </button>
  <button type="button" v-if="artists.length > 0" @click="showArtists = !showArtists">
    Discard artists
  </button>
</template>
