<script setup>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { AppState } from "../AppState.js";

const keeps = computed(() => AppState.keeps)

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  } catch (error) {
    Pop.toast('Could not get keeps', 'error')
    logger.error('Could not get keeps', error)
  }
}

onMounted(() =>
  getAllKeeps()
)
</script>

<template>
  <div class="home flex-grow-1 d-flex flex-column align-items-center justify-content-center">
    <div class="home-card p-5 card align-items-center shadow rounded elevation-3">
      <img src="https://bcw.blob.core.windows.net/public/img/8600856373152463" alt="CodeWorks Logo"
        class="rounded-circle">
      <h1 class="my-5 bg-dark text-white p-3 rounded text-center">
        Hiya 👋
      </h1>
      <div v-for="keep in keeps" :key="keep.id">
        <p>{{ keep.name }}</p>
        <img :src="keep.img" alt="">
      </div>
    </div>
  </div>
</template>

<style scoped lang="scss">
.home {
  display: grid;
  height: 80vh;
  place-content: center;
  text-align: center;
  user-select: none;

  .home-card {
    width: clamp(500px, 50vw, 100%);

    >img {
      height: 200px;
      max-width: 200px;
      width: 100%;
      object-fit: contain;
      object-position: center;
    }
  }
}
</style>
