<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import KeepWall from "../components/KeepWall.vue";
import { useRoute, useRouter } from "vue-router";
import { vaultsService } from "../services/VaultsService.js";
import HomePage from "./HomePage.vue";

const account = computed(() => AppState.account)

const route = useRoute()
const router = useRouter()
const activeVault = computed(() => AppState.activeVault)
const bgStyle = computed(() => `(url${activeVault.value?.img})`)

async function getVaultById() {
  try {
    // AppState.activeVault = null
    await vaultsService.setActiveVault(route.params.vaultId);

  } catch (error) {
    if (error.response.status !== 200) {
      Pop.toast(`Unauthorized to access this vault`)
      router.push({ name: "Home" })
    }
    else {
      Pop.error(error)
      logger.error('Unauthorized access', error)
    }
  }
}

async function destroyVault(vaultId) {
  try {
    const wantsToDestroy = await Pop.confirm(`Are you sure you want to delete your ${activeVault.value.name} vault? This cannot be undone.`)
    if (!wantsToDestroy) return
    await vaultsService.destroyVault(vaultId)
    router.push({ name: 'Home' })
  } catch (error) {
    Pop.error('Could not destroy vault')
    logger.error('Unable to destroy vault', error)
  }
}

// TODO consider supporting vault update
async function updateVault() {
  Pop.error('Feature not supported 🙅')
  logger.error('Vault update is not supported')
}

onMounted(() =>
  getVaultById()
)

</script>

<template>
  <div class="about text-center">
    <div class="container" v-if="activeVault">

      <div class="my-3">
        <div class="hero-section rounded rounded-3 shadow mt-2 mx-0 mb-5">

          <!-- <RoundProfilePhoto :profile="account" /> -->
          <!-- <img class="rounded" :src="activeVault.img" alt="" /> -->
          <h4>{{ activeVault.name }}</h4>
          <h4>By {{ account?.name }}</h4>
        </div>
        <div v-if="activeVault.creator.id == account?.id" class="text-end btn-group dropstart">
          <i role="button" class="mdi mdi-dots-horizontal fs-3" data-bs-toggle="dropdown" data-bs-auto-close="outside"
            aria-expanded="false"></i>

          <div>
            <ul class="dropdown-menu text-center w-auto p-1">
              <i role="button" @click="destroyVault(activeVault.id)" class="fs-3 m-2 mdi mdi-delete opacity-75"></i>
              <i role="button" @click="updateVault()" class="fs-3 m-2 mdi mdi-pencil opacity-75"></i>
              <!-- <FavoriteButton :vault="activeVault" /> -->
            </ul>
          </div>

        </div>
        <div v-else>
          <FavoriteButton :vault="activeVault" />
        </div>
      </div>
      <p class="rounded rounded-pill bg-info">XX Keeps</p>
      <div class="row"></div>
      <!-- TODO set a v-if to say that their keeps will appear here once they create some -->
      <h3>Keeps</h3>
      <KeepWall />
    </div>

    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>

<style scoped lang="scss">
.hero-section {
  height: 20dvh;
  // background-color: red;
  background-image: v-bind(bgStyle);
  // background-image: url(src/assets/img/calum-lewis-vA1L1jRTM70-unsplash.jpg);
  background-position: center;
  background-size: cover;
}



img {
  max-width: 100px;
}
</style>