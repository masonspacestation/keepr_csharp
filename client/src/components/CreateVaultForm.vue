<script setup>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";

const router = useRouter()


const vaultData = ref({
  name: 'test',
  description: 'ing',
  isPrivate: false,
  img: 'https://images.unsplash.com/photo-1518349619113-03114f06ac3a?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8dGVzdGluZ3xlbnwwfHwwfHx8MA%3D%3D',
})

function resetForm() {
  vaultData.value = {
    name: '',
    description: '',
    isPrivate: false,
    img: '',
  }
}
// FIXME take all recipe stuff off, implement function in service, and put in modal
async function createVault() {
  try {
    const newVault = await vaultsService.createVault(vaultData.value)
    resetForm()
    Modal.getOrCreateInstance('#create-vault-modal').hide()
    Pop.toast('New vault created!', 'success')
    router.push({ name: 'Active Vault', params: { vaultId: newVault.id } })
    // console.log('New Vault Created!', newVault);
  } catch (error) {
    Pop.toast('Could not create vault', 'error')
    logger.error('Could not create vault', error)
  }
}
</script>


<template>
  <form @submit.prevent="createVault()">
    <div class="container">

      <div class="row pt-4 pb-2 bg-success">
        <h3>
          Create a Vault!
        </h3>
      </div>
      <div class="row px-2 ps-lg-2 pe-lg-0 border-bottom">
        <div class="col-12 col-lg-8 pt-3 pe-lg-3 pt-lg-4 d-flex flex-column justify-content-around">
          <div>
            <div class="row form-floating p-1">
              <input v-model="vaultData.name" type="text" name="vault-name" id="vault-name" class="form-control mb-3"
                minlength="3" maxlength="50" placeholder="Vault Name" required>
              <label for="vault-name">Vault Name</label>
            </div>
            <div class="row form-floating p-1">
              <input v-model="vaultData.description" type="text" description="vault-title" id="vault-title"
                class="form-control mb-3" minlength="3" maxlength="50" placeholder="Vault Title" required>
              <label for="vault-title">Vault Description</label>
            </div>
            <div class="row form-floating p-1">
              <input v-model="vaultData.img" type="text" name="vault-img" id="vault-img" class="form-control mb-3"
                minlength="3" maxlength="1000" placeholder="Image URL" required>
              <label for="vault-img">Image URL</label>

            </div>

          </div>


          <div class="row ps-4">

            <div class="form-check form-switch w-auto mb-3">
              <input v-model="vaultData.isPrivate" class="form-check-input" type="checkbox" id="flexSwitchCheckDefault">
              <label class="form-check-label" for="isPrivate">Make vault private?</label>
            </div>
          </div>
        </div>

        <!-- end of form fields -->
        <div class="col-12 col-lg-4 px-2 p-lg-0 rounded rounded-2 mb-3">
          <img :src="vaultData.img" alt="" class="img-preview">
        </div>
        <!-- https://images.unsplash.com/photo-1582139329536-e7284fece509?w=1600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8dmF1bHR8ZW58MHx8MHx8fDA%3D -->

      </div>
    </div>
    <div class="row px-1 pb-2">
      <div class="col-12 col-lg-6 my-1 my-lg-3"><button type="reset" data-bs-toggle="modal"
          data-bs-target="#create-vault-modal"
          class="btn btn-outline-secondary border-2 text-dark w-100">Cancel</button></div>
      <div class="col-12 col-lg-6 my-1 my-lg-3"><button class="btn btn-primary w-100">Create Vault</button>
      </div>
    </div>


  </form>
</template>


<style lang="scss" scoped>
.img-preview {
  width: 100%;
  height: 25dvh;
  object-fit: cover;
  object-position: center;
}
</style>