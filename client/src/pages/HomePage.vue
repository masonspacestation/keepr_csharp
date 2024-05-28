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

async function getKeepById(keepId) {
  try {
    AppState.activeKeep = null
    console.log(`Setting ${keepId} to active`);
    await keepsService.getKeepById(keepId);
    // Modal.getOrCreateInstance('#keep-details-modal')
  } catch (error) {
    Pop.toast(`Could not get keep with ID: ${keepId}`)
    logger.error(`Could not get keep with ID: ${keepId}`, error)
  }
}


onMounted(() =>
  getAllKeeps(),
)
</script>

<template>
  <div class="container">
    <div class="row">
      <div v-for="keep in keeps" :key="keep.id" class="col-12 col-md-6 col-lg-4 py-3 px-4 masonry" role="button"
        data-bs-toggle="modal" data-bs-target="#keep-details-modal">


        <!-- <RoundProfilePhoto :profile="keep.creator" /> -->
        <!-- <KeepModal :keep="keep" /> -->
        <!-- </div> -->
        <!-- <KeepWall /> -->

        <KeepCard :keep="keep" @click="getKeepById(keep.id)" />

      </div>
      <!-- NOTE testing keep details independent of modal -->
      <!-- <div v-if="AppState.activeKeep">
        <KeepDetailsModal />
      </div> -->
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

.masonry {
  columns: 300px;
  column-gap: 1rem;
}
</style>
