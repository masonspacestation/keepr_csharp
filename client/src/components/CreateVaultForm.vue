<script setup>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";

const router = useRouter()


const vaultData = ref({
  name: '',
  description: '',
  isPrivate: false,
  img: '',
})

function resetForm() {
  vaultData.value = {
    name: '',
    description: '',
    isPrivate: false,
    img: '',
  }
}

async function createVault() {
  try {
    const newVault = await vaultsService.createVault(vaultData.value)
    Modal.getOrCreateInstance('#create-vault-modal').hide()
    router.push({ name: 'Active Vault', params: { vaultId: newVault.id } })
    Pop.toast('New vault created!', 'success')
    resetForm()

  } catch (error) {
    Pop.toast('Could not create vault', 'error')
    logger.error('Could not create vault', error)
  }
}
</script>


<template>
  <form @submit.prevent="createVault()">
    <div class="container">

      <div class="row">
        <div class="col-12 col-md-9">

          <h3 class="p-3 mb-0">
            Create a Vault!
          </h3>
          <div class="container">

            <div class="row px-2 px-0">
              <div class="col-12 col-lg-6 pt-3 pe-lg-3 pt-lg-0 d-flex flex-column justify-content-start">

                <!-- name -->
                <div class="row form-floating p-1">
                  <input v-model="vaultData.name" type="text" name="vault-name" id="vault-name"
                    class="form-control mb-3" minlength="3" maxlength="50" placeholder="Vault Name" required>
                  <label for="vault-name">Vault Name</label>
                </div>
                <!-- imgUrl -->
                <div class="row form-floating p-1">
                  <input v-model="vaultData.img" type="text" name="vault-img" id="vault-img"
                    class="form-control mb-3 mb-lg-0" minlength="3" maxlength="1000" placeholder="Image URL" required>
                  <label for="vault-img">Image URL</label>
                </div>


              </div>
              <!-- description -->
              <div class="col-12 col-lg-6 pt-3 pe-lg-3 pt-lg-0 d-flex flex-column justify-content-between">

                <div class="row form-floating p-1">
                  <input v-model="vaultData.description" type="text" description="vault-title" id="vault-title"
                    class="form-control mb-3" minlength="3" maxlength="50" placeholder="Vault Title" required>
                  <label for="vault-title">Vault Description</label>
                </div>
                <!-- isPrivate -->
                <div class="row p-4">
                  <div class="form-check form-switch w-auto mb-3 mb-lg-0">
                    <input v-model="vaultData.isPrivate" class="form-check-input" type="checkbox" id="isPrivate"
                      title="Switch to make your new vault private.">
                    <label class="form-check-label" for="isPrivate">Make vault private?</label>
                  </div>
                </div>
              </div>
            </div>
            <!-- buttons -->
            <div class="row mt-2 mb-3 px-1">
              <div class="col-12 col-lg-4 mt-2 mt-lg-0"><button @click="resetForm()" type="reset" data-bs-toggle="modal"
                  data-bs-target="#create-vault-modal" class="btn btn-outline-secondary border-2 text-dark w-100"
                  title="Cancel creation of vault">Cancel</button></div>
              <div class="col-12 col-lg-4 mt-2 mt-lg-0"><button class="btn btn-primary w-100"
                  :title="`Create vault ${vaultData.name}`">Create Vault</button>
              </div>
            </div>
          </div>
        </div>
        <!-- end of form fields -->
        <div class="col-12 col-lg-3 px-2 p-lg-0 rounded rounded-2 mb-3">
          <img :src="vaultData.img" alt="" class="img-preview" :title="`Preview of image for ${vaultData.name}`">
        </div>

      </div>
    </div>


  </form>
</template>


<style lang="scss" scoped>
.img-preview {
  // min-width: 100%;
  // height: 25dvh;
  object-fit: cover;
  object-position: center;
}
</style>