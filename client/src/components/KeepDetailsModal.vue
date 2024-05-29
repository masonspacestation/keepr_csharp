<script setup>
import { computed, ref } from "vue";
import { Keep } from "../models/Keep.js";
import RoundProfilePhoto from "./RoundProfilePhoto.vue";
import { AppState } from "../AppState.js";
import App from "../App.vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultKeepsService } from "../services/VaultKeepsService.js";

// defineProps({ keep: { type: Keep, required: true } })
const keep = computed(() => AppState.activeKeep)
const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)

// const keepKept = ref(keep.value?.kept)

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
</script>


<template>
  <div v-if="keep" class="container">
    <div class="row p-0">
      <div class="col-6 p-0">
        <!-- hiya 👋 -->
        <img :src="keep.img" alt="" class="">
      </div>
      <div class="col-6 p-4 d-flex flex-column justify-content-between align-items-center">
        <p class="text-secondary opacity-50"><i class="mdi mdi-eye-outline"></i> <span class="me-2"> {{ keep.views }}
          </span><span class="ms-2">𝌊 {{ keep.kept }}</span>
        </p>

        <div class="px-5 text-center">
          <h3 class="mb-3">{{ keep.name }}</h3>
          <p>{{ keep.description }}</p>
        </div>

        <div class="row w-100 align-items-center">
          <div class="col-6 text-center">
            <div v-if="account" class="row justify-content-between align-content-center">

              <form @submit.prevent="createVaultKeep()" class="py-3">
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
          <div class="col-6 ms-auto">
            <small class="w-auto">{{ keep.creator.name }}</small>
            <div class="">
              <RouterLink :to="{ name: 'Profile Page', params: { profileId: keep.creator.id } }">
                <RoundProfilePhoto :profile="keep.creator" role="button" data-bs-toggle="modal"
                  data-bs-target="#keep-details-modal" />
              </RouterLink>
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