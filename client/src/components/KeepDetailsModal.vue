<!-- eslint-disable no-console -->
<script setup>
import { computed, ref } from "vue";
import { Keep } from "../models/Keep.js";
import RoundProfilePhoto from "./RoundProfilePhoto.vue";
import { AppState } from "../AppState.js";
import App from "../App.vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";
import { Modal } from "bootstrap";

// defineProps({ keep: { type: Keep, required: true } })
const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)
const vaultKeeps = computed(() => AppState.activeVaultKeeps)

const vkId = computed(() => AppState.activeVaultKeeps.find((vk) => vk.id == keep.value.id)?.vaultKeepId)

const vkData = ref({
  vaultId: 0,
  keepId: keep.value?.id,
})

function resetForm() {
  vkData.value = {
    vaultId: 0,
    keepId: keep.value?.id,
  }
}

function setVkId() {
  keep.value.vaultKeepId = vkId
}

async function createVaultKeep() {
  try {
    await vaultKeepsService.createVaultKeep(vkData.value)
    Pop.toast('Kept new keep!', 'success')
    resetForm()
    //  TODO increment vk count of keep
    // TODO change "add keep" form to a router link to the vault and a button to "unkeep" it
  } catch (error) {
    Pop.toast('Could not keep this keep.', 'error')
    logger.error('Could not keep this keep.', error)
  }
}

async function destroyVaultKeep() {
  try {
    const wantsToDelete = await Pop.confirm('Are you sure you want to remove this most excellent post from your vault?')
    if (!wantsToDelete) { return }

    await setVkId()
    await vaultKeepsService.destroyVaultKeep(vkId.value)
    Modal.getOrCreateInstance('#keep-details-modal').hide()
    Pop.toast('Your vaultKeep was deleted!', 'success')
    console.log('Deleting vaultKeep :', vkId);
  } catch (error) {
    Pop.toast('Could not delete this vaultKeep.', 'error')
    logger.error('Could not delete this vaultKeep.', error)
  }
}

</script>


<template>
  <div v-if="keep" class="container">
    <div class="row p-0">
      <div class="col-12 col-md-6 p-0">
        <!-- hiya 👋 -->
        <img :src="keep.img" alt="" class="">
      </div>

      <div class="col-12 col-md-6 p-4 d-flex flex-column justify-content-between align-items-center">
        <p class="text-secondary opacity-50"><i class="mdi mdi-eye-outline"></i> <span class="me-2"> {{ keep.views }}
          </span><span class="ms-2">𝌊 {{ keep.kept }}</span>
        </p>

        <div class="px-5 text-center">
          <h3 class="mb-3">{{ keep.name }}</h3>
          <p>{{ keep.description }}</p>
        </div>
        <div class="container-fluid">

          <div class="row w-100 align-items-center">
            <div v-if="vaultKeeps.some(vk => vk.id == keep.id)" class="col-md-6 text-center">
              <div v-if="account" class="row justify-content-between align-content-center">
                <button @click="destroyVaultKeep()" class="w-50 btn btn-link fw-bold text-secondary opacity-50"><i
                    class="mdi mdi-cancel"></i>
                  Remove</button>
              </div>
            </div>

            <!-- SECTION for keeps that aren't in the current vault, will be shown on pages outside the active vault page -->
            <div v-else>
              <div v-if="account" class="row justify-content-between align-content-center">

                <form @submit.prevent="createVaultKeep()" class="">
                  <select v-model="vkData.vaultId" class="form-select w-50">
                    <option value="" selected>Keep this</option>
                    <option v-for="vault in myVaults" :key="vault.id" :value="vault.id" name="keep-to-vault"
                      id="keep-to-vault">{{
                        vault.name }}</option>
                  </select>
                  <button type="submit" class="w-50 btn btn-primary">Keep</button>
                </form>
              </div>
            </div>
          </div>


          <div class="col-md-6 d-flex flex-column justify-content-around h-100 align-items-center">
            <div class="">
              <RouterLink :to="{ name: 'Profile Page', params: { profileId: keep.creator.id } }">
                <RoundProfilePhoto class="my-1" :profile="keep.creator" role="button" data-bs-toggle="modal"
                  data-bs-target="#keep-details-modal" />
              </RouterLink>
              <p class="m-0">{{ keep.creator.name }}</p>
            </div>

          </div>
        </div>
        <!-- </div> -->
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
img {
  width: 100%;
  object-fit: cover;
  object-position: center;
}
</style>