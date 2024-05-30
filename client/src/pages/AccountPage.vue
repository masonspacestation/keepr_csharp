<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import KeepWall from "../components/KeepWall.vue";
import { Modal } from "bootstrap";
import ModalWrapper from "../components/ModalWrapper.vue";
import AccountForm from "../components/AccountForm.vue";

const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)
const keeps = computed(() => AppState.myKeeps)
const bgStyle = computed(() => `url(${account.value?.coverImg})`)

// async function getMyVaults() {
//   try {
//     AppState.myVaults = null
//     await accountService.getMyVaults()
//   } catch (error) {
//     Pop.toast('Could not get your vaults', 'error')
//     logger.error('Error getting your vaults', error)

//   }
// }


// onMounted(() =>
//   getMyVaults()
// )

</script>

<template>
  <div class="about text-center">
    <div class="container w-75" v-if="account">


      <div class="my-3">
        <div class="hero-section rounded rounded-3 shadow mt-5 w-75 mx-auto">
        </div>
        <div class="id-module d-block">
          <RoundProfilePhotoLarge :profile="account" class="mb-3" />
          <h3>{{ account.name }}</h3>
          <p>{{ myVaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
        </div>
      </div>

      <div class="row justify-content-end">
        <button class="btn btn-outline-secondary opacity-50 w-auto me-3 mb-3"><i class="mdi mdi-pencil"
            data-bs-toggle="modal" data-bs-target="#update-account-modal" data-bs-dismiss="modal"></i></button>
        <AddItemFloatingButton />
      </div>

      <!-- <AccountForm /> -->
      <!-- <div v-if="profile?.id == account?.id" class="row"> -->

      <!-- TODO set a v-if to say that their vaults will appear here once they create some -->
      <h3>Vaults</h3>
      <div class="row">
        <div v-for="vault in myVaults" :key="vault.id" class="col-6 col-md-3">
          <VaultCard :vault="vault" />
        </div>
      </div>

      <!-- TODO set a v-if to say that their keeps will appear here once they create some -->
      <h3>Keeps</h3>
      <div class="masonry my-3">
        <div v-for="keep in keeps" :key="keep?.id" class="mb-3">
          <KeepCard :keep="keep" />
        </div>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <!-- <ModalWrapper modalId="create-vault-modal">
    <CreateVaultForm />
  </ModalWrapper>
  <ModalWrapper modalId="create-keep-modal">
    <CreateKeepForm />
  </ModalWrapper>
  <ModalWrapper modalId="update-account-modal">
    <AccountForm />
  </ModalWrapper> -->
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}

.hero-section {
  height: 20dvh;
  // background-color: red;
  background-image: v-bind(bgStyle);
  // background-image: url(src/assets/img/calum-lewis-vA1L1jRTM70-unsplash.jpg);
  background-position: center;
  background-size: cover;
  margin-bottom: -50px;
}

.masonry {
  columns: 200px;
  column-gap: 1em;
}
</style>
