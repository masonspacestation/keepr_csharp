<script setup>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { vaultsService } from "../services/VaultsService.js";


const vaultData = ref({
  name: '',
  description: '',
  isPrivate: '',
  img: '',
})

function resetForm() {
  vaultData.value = {
    name: '',
    description: '',
    isPrivate: '',
    img: '',
  }
}

async function createVault() {
  try {
    const newVault = vaultsService.createVault(vaultData.value)

  } catch (error) {
    Pop.toast('Could not create vault', 'error')
    logger.error('Could not create vault', error)
  }
}
</script>


<template>
  <form @submit.prevent="createVault">
    <div class="container">

      <div class="row pt-4 pb-2 bg-success">
        <h3>
          Create a Vault!
        </h3>
      </div>
      <div class="row px-2 ps-lg-2 pe-lg-0 border-bottom">
        <div class="col-12 col-lg-8 pt-3 pe-lg-3 pt-lg-4 d-flex flex-column justify-content-around">

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

          <div class="col-12 col-lg-6 form-floating p-1">
            <input v-model="vaultData.img" type="text" name="vault-img" id="vault-img" class="form-control mb-3"
              minlength="3" maxlength="1000" placeholder="Image URL" required>
            <label for="vault-img">Image URL</label>
          </div>
        </div>

        <!-- <hr class="border-2 border-success opacity-100 m-0 p-0"> -->
      </div>
      <!-- end of form fields -->

      <div class="col-12 col-lg-4 px-2 p-lg-0">
        <img :src="vaultData.img" alt="" class="img-preview">
      </div>
    </div>


    <div class="row px-1 pb-2">
      <div class="col-12 col-lg-6 my-1 my-lg-3"><button type="reset" data-bs-toggle="modal" data-bs-target="modal"
          class="btn btn-outline-secondary border-2 text-dark w-100">Cancel</button></div>
      <div class="col-12 col-lg-6 my-1 my-lg-3"><button class="btn btn-primary w-100">Create Recipe</button>
      </div>
    </div>


  </form>
</template>


<style lang="scss" scoped></style>