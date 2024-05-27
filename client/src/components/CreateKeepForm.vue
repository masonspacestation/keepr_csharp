<script setup>
import { ref } from "vue";
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { Modal } from "bootstrap";
import { useRouter } from "vue-router";
import { keepsService } from "../services/KeepsService.js";

const router = useRouter()


const keepData = ref({
  name: 'test',
  description: 'ing',
  img: 'https://images.unsplash.com/photo-1616458964840-5108e4d3adb3?w=800&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fHRlc3Rpbmd8ZW58MHx8MHx8fDA%3D',
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
    resetForm()
    Modal.getOrCreateInstance('#create-keep-modal').hide()
    Pop.toast('New keep created!', 'success')
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

      <div class="row pt-4 pb-2 bg-success">
        <h3>
          Create a Keep!
        </h3>
      </div>
      <div class="row px-2 ps-lg-2 pe-lg-0 border-bottom">
        <div class="col-12 col-lg-8 pt-3 pe-lg-3 pt-lg-4 d-flex flex-column justify-content-around">
          <div>
            <div class="row form-floating p-1">
              <input v-model="keepData.name" type="text" name="keep-name" id="keep-name" class="form-control mb-3"
                minlength="3" maxlength="50" placeholder="Keep Name" required>
              <label for="keep-name">Keep Name</label>
            </div>
            <div class="row form-floating p-1">
              <input v-model="keepData.description" type="text" description="keep-title" id="keep-title"
                class="form-control mb-3" minlength="3" maxlength="50" placeholder="Keep Title" required>
              <label for="keep-title">Keep Description</label>
            </div>
            <div class="row form-floating p-1">
              <input v-model="keepData.img" type="text" name="keep-img" id="keep-img" class="form-control mb-3"
                minlength="3" maxlength="1000" placeholder="Image URL" required>
              <label for="keep-img">Image URL</label>
            </div>
          </div>
        </div>

        <!-- end of form fields -->
        <div class="col-12 col-lg-4 px-2 p-lg-0 rounded rounded-2 mb-3">
          <img :src="keepData.img" alt="" class="img-preview">
        </div>
        <!-- https://images.unsplash.com/photo-1582139329536-e7284fece509?w=1600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8dmF1bHR8ZW58MHx8MHx8fDA%3D -->

      </div>
    </div>
    <div class="row px-1 pb-2">
      <div class="col-12 col-lg-6 my-1 my-lg-3"><button type="reset" data-bs-toggle="modal"
          data-bs-target="#create-keep-modal" class="btn btn-outline-secondary border-2 text-dark w-100">Cancel</button>
      </div>
      <div class="col-12 col-lg-6 my-1 my-lg-3"><button class="btn btn-primary w-100">Create Keep</button>
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