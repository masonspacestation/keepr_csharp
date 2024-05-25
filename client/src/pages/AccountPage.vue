<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";

const account = computed(() => AppState.account)

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
    <div v-if="account">
      <h1>Welcome {{ account.name }}</h1>
      <img class="rounded" :src="account.picture" alt="" />
      <p>{{ account.email }}</p>
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
