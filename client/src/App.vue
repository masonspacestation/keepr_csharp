<script setup>
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { Modal } from "bootstrap";
import ModalWrapper from "./components/ModalWrapper.vue";
import CreateVaultForm from "./components/CreateVaultForm.vue";
import AccountForm from "./components/AccountForm.vue";
import { keepsService } from "./services/KeepsService.js";
import Pop from "./utils/Pop.js";
import { logger } from "./utils/Logger.js";
import { onMounted } from "vue";


async function getAllKeeps() {
  try {
    await keepsService.getAllKeeps()
  } catch (error) {
    Pop.toast('Could not get keeps', 'error')
    logger.error('Could not get keeps', error)
  }
}



onMounted(() =>
  getAllKeeps(),
)
</script>

<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <!-- <footer class="bg-dark text-light">
    Made with 💖 by CodeWorks
  </footer> -->
  <ModalWrapper modalId="keep-details-modal">
    <KeepDetailsModal />
  </ModalWrapper>

  <ModalWrapper modalId="create-vault-modal">
    <CreateVaultForm />
  </ModalWrapper>

  <ModalWrapper modalId="create-keep-modal">
    <CreateKeepForm />
  </ModalWrapper>

  <ModalWrapper modalId="update-account-modal">
    <AccountForm />
  </ModalWrapper>

</template>


<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
