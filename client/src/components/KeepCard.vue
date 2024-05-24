<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";

// const keep = computed(() => AppState.activeKeep)
// const keeps = computed(() => AppState.keeps)
const props = defineProps({ keep: { type: Keep, required: true } })
const bgStyle = computed(() => `url(${props.keep.img})`)

async function getKeepById(keepId) {
  try {
    AppState.activeKeep = null
    console.log('getting active keep', keepId);
    await keepsService.setActiveKeep(keepId);

  } catch (error) {
    Pop.toast(`Could not get keep with ID: ${keepId}`)
    logger.error(`Could not get keep with ID: ${keepId}`, error)
  }
}


</script>


<template>
  <!-- FIXME the p tag is getting the background set, and the image is above that, so it looks like 2 images stacked on top of each other. If I take aay the data-bound image, and just rely in the background property for the image, the card shrinks to the height of the p tag -->
  <div class="mb-3 keep-card rounded rounded-2 shadow" @click="getKeepById(keep.id)">
    <img :src="keep.img" alt="">
    <div>
      <h4>{{ keep.name }}</h4>
      <p>{{ keep.creatorId }}</p>
    </div>
  </div>
</template>


<style lang="scss" scoped>
img {
  width: 100%;
}

.keep-card {
  // background-image: v-bind(bgStyle);
  // background-size: cover;
  // background-position: center;

}
</style>