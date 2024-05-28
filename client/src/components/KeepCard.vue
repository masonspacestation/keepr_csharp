<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import { Modal } from "bootstrap";

// const keep = computed(() => AppState.activeKeep)
// const keeps = computed(() => AppState.keeps)
const props = defineProps({ keep: { type: Keep, required: true } })
const bgStyle = computed(() => `url(${props.keep.img})`)

async function getKeepById(keepId) {
  try {
    AppState.activeKeep = null
    console.log('getting active keep', keepId);
    await keepsService.getKeepById(keepId);
    // Modal.getOrCreateInstance('#keep-details-modal')
  } catch (error) {
    Pop.toast(`Could not get keep with ID: ${keepId}`)
    logger.error(`Could not get keep with ID: ${keepId}`, error)
  }
}




</script>


<template>
  <!-- FIXME the p tag is getting the background set, and the image is above that, so it looks like 2 images stacked on top of each other. If I take away the data-bound image, and just rely in the background property for the image, the card shrinks to the height of the p tag — hacky way it's working is that the image is there, but set to hidden -->
  <div class="container mb-4 keep-card rounded rounded-2 shadow" @click="getKeepById(keep.id)">
    <!-- data-bs-toggle="modal"
    data-bs-target="#keep-details-modal" -->
    <!--   <div class=" col-6 p-2 d-inline"> <button class="btn btn-outline-primary" data-bs-toggle="modal"
            data-bs-target="#create-keep-modal">Add Keep</button></div> -->
    <div class="row justify-content-between align-items-center p-3">
      <img class="bg-size" :src="keep.img" :alt="`Image of ${keep.name}`">
      <div class="col-9">
        <h4 class="my-0 text-light">{{ keep.name }}</h4>
      </div>
      <div class="col-3">
        <RoundProfilePhoto :profile="keep.creator" />
      </div>
    </div>
    <!-- <img :src="keep.creator.picture" :alt="`Image of ${keep.creator.name}`"> -->
  </div>
</template>


<style lang="scss" scoped>
img {
  width: 100%;
}

.keep-card {
  background-image: linear-gradient(rgba(0, 0, 0, 0.0), rgba(0, 0, 0, 0.2)), v-bind(bgStyle);
  background-size: cover;
  background-position: center;

  .bg-size {
    visibility: hidden;
  }
}
</style>