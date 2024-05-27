<script setup>
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import { Modal } from "bootstrap";
import ModalWrapper from "./components/ModalWrapper.vue";
import CreateVaultForm from "./components/CreateVaultForm.vue";


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
  <!-- <ModalWrapper>
    <KeepDetailsModal modalId="keep-details-modal" />
  </ModalWrapper> -->
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
