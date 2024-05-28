<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import { Vault } from "../models/Vault.js";
import { vaultsService } from "../services/VaultsService.js";

// const activeVault = computed(() => AppState.activeVault)
const props = defineProps({ vault: { type: Vault, required: true } })
const bgStyle = computed(() => `url(${props.vault.img})`)

// async function getVaultById(vaultId) {
//   try {
//     AppState.activeVault = null
//     console.log('getting active vault', vaultId);
//     await vaultsService.setActiveVault(vaultId);

//   } catch (error) {
//     Pop.toast(`Could not get vault with ID: ${vaultId}`)
//     logger.error(`Could not get vault with ID: ${vaultId}`, error)
//   }
// }


</script>


<template>
  <RouterLink :to="{ name: 'Active Vault', params: { vaultId: vault.id } }" :vault="vault">
    <div class="container mb-4 vault-card rounded rounded-2 shadow">
      <div class="row h-100 justify-content-center align-items-center p-3">
        <!-- <img :src="vault.img" alt=""> -->
        <h4 class="my-0 text-light">{{ vault.name }}</h4>
      </div>
    </div>
  </RouterLink>
</template>


<style lang="scss" scoped>
img {
  width: 100%;
}

.vault-card {
  background-image: linear-gradient(rgba(0, 0, 0, 0.2), rgba(0, 0, 0, 0.4)), v-bind(bgStyle);
  background-size: cover;
  background-position: center;
  height: 10dvh;

}
</style>