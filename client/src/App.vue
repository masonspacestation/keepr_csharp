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
  <main class="pb-4">
    <router-view />
  </main>
  <footer class="bg-dark">
    <div class="container justify-content-center">
      <FooterLinks />
    </div>
  </footer>

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
  min-height: 48px;
}

.icon {
  height: 32px;
  width: 32px;
  margin: 0;
  padding: 0;
}
</style>
