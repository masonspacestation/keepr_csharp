<script setup>
import { computed, onMounted, watch } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "../utils/Pop.js";
import { logger } from "../utils/Logger.js";
import { accountService } from "../services/AccountService.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import KeepWall from "../components/KeepWall.vue";
import { Modal } from "bootstrap";
import ModalWrapper from "../components/ModalWrapper.vue";
import AccountForm from "../components/AccountForm.vue";
import ProfileOwnerButtons from "../components/ProfileOwnerButtons.vue";

const account = computed(() => AppState.account)
const myVaults = computed(() => AppState.myVaults)
const keeps = computed(() => AppState.myKeeps)

// const keeps = computed(() => AppState.keeps.find((keep) => keep.creatorId == account.value.id)?.id)
const bgStyle = computed(() => `url(${account.value?.coverImg})`)

// watch(AppState.keeps.length, )
</script>

<template>
  <div class="about text-center">
    <div class="container w-md-75" v-if="account">

      <div class="my-3">
        <div class="hero-section rounded rounded-3 shadow mt-5 w-md-75 mx-auto row justify-content-end align-items-end">

          <button class="edit-button btn btn-outline-white btn-link w-auto me-3 mb-3"><i class="mdi mdi-pencil"
              data-bs-toggle="modal" data-bs-target="#update-account-modal" data-bs-dismiss="modal"
              title="Edit your account details"></i></button>
        </div>
        <div class="id-module ">
          <RoundProfilePhotoLarge :profile="account" class="mb-3 mx-auto" />
          <h3>{{ account.name }}</h3>
          <p>{{ myVaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
        </div>
      </div>
      <!-- FIXME the sizing of the dropdown container gets narrow any time this is directly inside a div. The buttons either get narrow and the text inside them stacks, or the buttons stack on each other. I'd like to change this to a fab if I have time at the end -->

      <AddItemFloatingButton />

      <div class="row justify-content-end">
        <!-- <ProfileOwnerButtons :profile="account" /> -->
      </div>

      <!-- <AccountForm /> -->
      <!-- <div v-if="profile?.id == account?.id" class="row"> -->

      <!-- TODO set a v-if to say that their vaults will appear here once they create some -->
      <h3 class="mt-5 fs-2">Vaults</h3>
      <div class="row">
        <div v-for="vault in myVaults" :key="vault.id" class="col-6 col-md-3">
          <VaultCard :vault="vault" />
        </div>
      </div>
      <div v-if="keeps">
        <!-- TODO set a v-if to say that their keeps will appear here once they create some -->
        <h3 class="mt-5 fs-2">Keeps</h3>
        <div class="masonry my-3">
          <div v-for="keep in keeps" :key="keep?.id" class="mb-3">
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
      <div v-else>
        <h4>Create some keeps and they'll show up here!</h4>
        <p>(Use the button up there)</p>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <!-- <KeepWall :keeps="keeps" /> -->
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}

.fab {
  position: absolute;
  bottom: 50px;
  right: 1em;
}

.hero-section {
  height: 20dvh;
  margin-bottom: -50px;
  background-image: v-bind(bgStyle);
  background-position: center;
  background-size: cover;
}


.masonry {
  column-gap: 1em;

  @media (min-width: 768px) {
    columns: 250px;
    // column-count: 2;
  }

  @media (max-width: 768px) {
    column-count: 2;
  }
}
</style>
