<!-- eslint-disable no-console -->
<script setup>
import { computed, onMounted } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { keepsService } from "../services/KeepsService.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import { Modal } from "bootstrap";

// const keep = computed(() => AppState.activeKeep)
// const keeps = computed(() => AppState.keeps)
const account = computed(() => AppState.account)
const props = defineProps({ keep: { type: Keep, required: true } })
const bgStyle = computed(() => `url(${props.keep.img})`)

async function getKeepById(keepId) {
  try {
    AppState.activeKeep = null
    console.log(`Setting ${keepId} to active`);
    await keepsService.getKeepById(keepId);
    Modal.getOrCreateInstance('#keep-details-modal').show()
    console.log('keep here', keepId);
  } catch (error) {
    Pop.toast(`Could not get keep with ID: ${keepId}`)
    logger.error(`Could not get keep with ID: ${keepId}`, error)
  }
}

async function destroyKeep(keepId) {
  try {
    const wantsToDelete = await Pop.confirm('Are you sure you want to delete this excellent Keep?', 'confirm')
    if (wantsToDelete != true) { return }

    await keepsService.destroyKeep(keepId)
    Pop.toast('Your Keep was deleted!', 'success')
    console.log('Keep was destroyed: ', keepId);
  } catch (error) {
    Pop.toast(`Could not delete keep with ID: ${keepId}`)
    logger.error(`Could not delete keep with ID: ${keepId}`, error)

  }
}

// NOTE this card is embedded in profile page, account page, and home page

</script>


<template>
  <!-- FIXME the p tag is getting the background set, and the image is above that, so it looks like 2 images stacked on top of each other. If I take away the data-bound image, and just rely in the background property for the image, the card shrinks to the height of the p tag — hacky way it's working is that the image is there, but set to hidden -->
  <div class="container mb-4 keep-card rounded rounded-2 shadow" @click="getKeepById(keep.id)">
    <!-- role="button" data-bs-toggle="modal"
      data-bs-target="#keep-details-modal"  -->
    <div class="row justify-content-between align-items-center p-0">
      <div class="delete-button-row"><i v-if="keep.creatorId == account?.id" @click.stop="destroyKeep(keep.id)"
          role="button" class="delete-button mdi mdi-close"></i></div>
      <img class="bg-size" :src="keep.img" :alt="`Image of ${keep.name}`">
      <div class="card-deets">
        <div class="col-12 col-md-9">
          <h4 class="my-0 text-light">{{ keep.name }}</h4>
        </div>
        <div class="col-3 d-none d-md-block">
          <RoundProfilePhoto :profile="keep.creator" />
        </div>
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
  // background-size: cover;
  // background-position: center;
  -webkit-column-break-inside: avoid;
  page-break-inside: avoid;
  break-inside: avoid;
  position: relative;
  overflow: hidden;
  background-color: black;

  .bg-size {
    mask-image: linear-gradient(rgba(0, 0, 0, 0.9), rgba(0, 0, 0, 0.6));
    max-width: 100%;
    margin: 0;
    padding: 0;
  }

  .delete-button {
    display: none;
  }
}

.card-deets {
  margin: 0;
  padding: .5em;
  position: absolute;
  bottom: 0;
}

.keep-card:hover {
  .delete-button {
    background-color: rgb(202, 116, 116);
    color: whitesmoke;
    border-radius: 50%;
    width: 25px;
    aspect-ratio: 1/1;
    display: flex;
    justify-content: center;
    align-items: center;
    margin: 0;
    position: absolute;
    top: 1em;
    left: 1em;
    z-index: 2;
  }
}
</style>