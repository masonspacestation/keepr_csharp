<script setup>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";

const keeps = computed(() => AppState.keeps)

async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  } catch (error) {
    Pop.toast('Could not get keeps', 'error')
    logger.error('Could not get keeps', error)
  }
}
// async function getMyVaults() {
//   try {
//     await accountService.getMyVaults()
//   } catch (error) {
//     Pop.error(error)
//   }
// }

onMounted(() =>
  getAllKeeps(),
  // getMyVaults()
)
</script>

<template>
  <div class="container">

    <div v-for="keep in keeps" :key="keep.id">
      <!-- <RoundProfilePhoto :profile="keep.creator" /> -->
      <!-- <KeepModal :keep="keep" /> -->
    </div>
    <KeepWall />


    <!-- <div class="home-card p-5 card align-items-center shadow rounded elevation-3"> -->

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

// .masonry {
//   columns: 300px;
//   column-gap: 1rem;
// }</style>
