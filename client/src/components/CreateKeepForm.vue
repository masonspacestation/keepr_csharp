<script setup>
import { computed, ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";
import { keepsService } from "../services/KeepsService.js";

const router = useRouter()
const bgStyle = computed(() => `url(${keepData.value?.img})`)

const keepData = ref({
  name: '',
  description: '',
  img: '',
})

function resetForm() {
  keepData.value = {
    name: '',
    description: '',
    img: '',
  }
}
async function createKeep() {
  try {
    const newKeep = await keepsService.createKeep(keepData.value)
    Modal.getOrCreateInstance('#create-keep-modal').hide()
    Pop.toast('New keep created!', 'success')
    resetForm()
    // TODO consider making this convert the modal to the keep details modal
    // router.push({ name: 'Active Keep', params: { keepId: newKeep.id } })
  } catch (error) {
    Pop.toast('Could not create keep', 'error')
    logger.error('Could not create keep', error)
  }
}
</script>


<template>
  <form @submit.prevent="createKeep()">
    <div class="container">

      <div class="row">
        <div class="col-12 col-lg-9">

          <h3 class="p-3 mb-0">
            Create a Keep!
          </h3>
          <div class="container">

            <div class="row px-2 px-0">
              <div class="col-12 col-lg-6 pt-3 pe-lg-3 pt-lg-0 d-flex flex-column justify-content-center">

                <!-- name -->
                <!-- <div class="row form-floating p-1">
            <input v-model="keepData.name" type="text" name="vault-name" id="vault-name"
              class="form-control mb-3" minlength="3" maxlength="50" placeholder="Vault Name" required>
            <label for="vault-name">Vault Name</label>
          </div> -->
                <div class="row form-floating p-1">
                  <input v-model="keepData.name" type="text" name="keep-name" id="keep-name" class="form-control mb-3"
                    minlength="3" maxlength="50" placeholder="Keep Name" required>
                  <label for="keep-name">Keep Name</label>
                </div>
                <!-- imgUrl -->
                <div class="row form-floating p-1">
                  <input v-model="keepData.img" type="text" name="keep-img" id="keep-img"
                    class="form-control mb-3 mb-lg-0" minlength="3" maxlength="1000" placeholder="Image URL" required>
                  <label for="keep-img">Image URL</label>
                </div>


              </div>
              <!-- description -->
              <div class="col-12 col-lg-6 pt-3 pe-lg-3 pt-lg-0 d-flex flex-column justify-content-between">

                <!-- <div class="row form-floating p-1">
                  <input v-model="keepData.description" type="text" description="vault-title" id="vault-title"
                    class="form-control mb-3" minlength="3" maxlength="50" placeholder="Vault Title" required>
                  <label for="vault-title">Vault Description</label>
                </div> -->
                <div class="row form-floating p-1">
                  <input v-model="keepData.description" type="text" description="keep-description" id="keep-description"
                    class="form-control mb-3" minlength="3" maxlength="50" placeholder="Keep Description" required>
                  <label for="keep-description">Keep Description</label>
                </div>

              </div>
            </div>
            <!-- buttons -->
            <div class="row mt-2 mb-3 px-1">
              <div class="col-12 col-lg-4 mt-2 mt-lg-0"><button @click="resetForm()" type="reset" data-bs-toggle="modal"
                  data-bs-target="#create-keep-modal" class="btn btn-outline-secondary border-2 text-dark w-100"
                  title="Cancel creation of keep">Cancel</button></div>
              <div class="col-12 col-lg-4 mt-2 mt-lg-0"><button class="btn btn-primary w-100"
                  :title="`Create keep ${keepData.name}`">Create
                  Keep</button>
              </div>
            </div>
          </div>
        </div>

        <div class="col-12 col-lg-3 p-0 p-lg-0 img-preview m-lg-0">
          <!-- <img :src="keepData.img" alt="" class="img-preview" :title="`Preview of image for ${keepData.name}`"> -->
        </div>
      </div>
    </div>

  </form>
</template>


<style lang="scss" scoped>
.img-preview {
  // min-width: 100%;
  // object-fit: cover;
  // object-position: center;
  // max-height: 100%;
  overflow: hidden;
  background-image: v-bind(bgStyle);
  background-size: cover;
  background-position: center;
}
</style>