<script lang="ts">
  type Artist = {
    name: string;
    genre: string;
  };
</script>

<script setup lang="ts">
  import { computed, ref } from 'vue';

  const artists = ref<Artist[]>([]);
  const filter = ref<string>('');
  const showArtists = ref(true);

  async function getArtists() {
    artists.value = await (await fetch('/test/artists')).json();
  }

  const genres = computed(() => getDistinctGenres());

  const filteredArtists = computed(() => {
    if (!filter.value) {
      return artists.value;
    }

    return artists.value.filter((artist) => artist.genre === filter.value);
  });

  function getDistinctGenres() {
    return artists.value.reduce((genres, artist) => {
      if (!genres.includes(artist.genre)) {
        genres.push(artist.genre);
      }
      return genres;
    }, [] as string[]);
  }

  function addArtist() {
    const newArtist: Artist = {
      name: 'Dua Lipa <3',
      genre: 'Pop',
    };
    artists.value.push(newArtist);
  }

  function setFilter(genre: string) {
    filter.value = genre;
  }
</script>

<template>
  <h1>Artists</h1>
  <ul v-if="filteredArtists.length && showArtists">
    <li v-for="artist in filteredArtists" :key="artist.name">
      {{ artist.genre }} artist {{ artist.name }}
    </li>
  </ul>
  <ul>
    <li v-for="genre in genres" :key="genre">
      <button @click="setFilter(genre)">{{ genre }}</button>
    </li>
  </ul>
  {{ filter }}
  <button v-if="artists.length === 0" @click="getArtists()">Show me the artists</button>
  <button v-if="artists.length > 0" @click="showArtists = !showArtists">Discard artists</button>

  <button @click="addArtist">Add artist</button>
</template>
