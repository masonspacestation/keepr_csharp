<!-- eslint-disable no-console -->
<script setup>
import { computed, onMounted } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { keepsService } from "../services/KeepsService.js";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import KeepCard from "../components/KeepCard.vue";
import KeepDetailsModal from "../components/KeepDetailsModal.vue";
import { Modal } from "bootstrap";

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
  getAllKeeps(),
)
</script>

<template>
  <!-- NOTE testing keep details independent of modal -->
  <!-- <div v-if="AppState.activeKeep">
    <KeepDetailsModal />
  </div> -->

  <div class="container w-75">
    <div class="masonry my-3">
      <!-- <div v-for="keep in keeps" :key="keep.id" class="col-12 col-md-6 col-lg-4 py-3 px-4"> -->
      <KeepCard class="keep-card mb-3" :keep="keep" v-for="keep in keeps" :key="keep.id" />


    </div>
  </div>


</template>

<style scoped lang="scss">
.masonry {
  columns: 200px;
  column-gap: 1em;

  // .keep-card {
  //   min-height: 300px;
  // }

  // .keep-card:nth-child(2n) {
  //   min-height: 500px;
  // }

  // .keep-card:nth-child(3n) {
  //   min-height: 300px;
  // }

  // .keep-card:nth-child(4n) {
  //   min-height: 800px;
  // }
}
</style>
