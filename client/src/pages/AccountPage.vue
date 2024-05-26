<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import KeepWall from "../components/KeepWall.vue";

const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)

async function getMyVaults() {
  try {
    AppState.myVaults = null
    await accountService.getMyVaults()
  } catch (error) {
    Pop.toast('Could not get your vaults', 'error')
    logger.error('Error getting your vaults', error)

  }
}


onMounted(() =>
  getMyVaults()
)

</script>

<template>
  <div class="about text-center">
    <div class="container" v-if="account">


      <div class="my-3">
        <RoundProfilePhoto :profile="account" />
        <!-- <img class="rounded" :src="account.picture" alt="" /> -->
        <h1>{{ account.name }}</h1>
        <p>{{ myVaults?.length }} Vaults | XX Keeps</p>
      </div>
      <div class="row justify-content-end">
        <AddItemFloatingButton />
      </div>

      <!-- TODO set a v-if to say that their vaults will appear here once they create some -->
      <h3>Vaults</h3>
      <div class="row">
        <div v-for="vault in myVaults" :key="vault.id" class="col-6 col-md-3">
          <VaultCard :vault="vault" />
        </div>
      </div>

      <!-- TODO set a v-if to say that their keeps will appear here once they create some -->
      <h3>Keeps</h3>
      <!-- <KeepWall /> -->
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}
</style>
