<!-- eslint-disable no-console -->
<script setup>
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import Pop from "../utils/Pop.js";
import RoundProfilePhoto from "../components/RoundProfilePhoto.vue";
import KeepCard from "../components/KeepCard.vue";
import ModalWrapper from "../components/ModalWrapper.vue";
import { useRoute } from "vue-router";
import { profilesService } from "../services/ProfilesService.js";


const route = useRoute()

const profile = computed(() => AppState.activeProfile)
const account = computed(() => AppState.account)
const keeps = computed(() => AppState.profileKeeps)
// const keeps = computed(() => AppState.keeps.find((keep) => keep.creatorId == profile.value.id)?.id)
const vaults = computed(() => AppState.profileVaults)

const bgStyle = computed(() => `url(${profile.value?.coverImg})`)


async function setActiveProfile() {
  try {
    await profilesService.setActiveProfile(route.params.profileId)
  } catch (error) {
    Pop.toast('Could not get profile', 'error')
    console.log(`Could not get profile for id: ${route.params.profileId}`)
  }
}

async function getProfileVaults() {
  try {
    await profilesService.getProfileVaults(route.params.profileId)
  } catch (error) {
    Pop.toast("Could not get profile's vaults", 'error')
    console.log(`Could not get vaults for profile id: ${route.params.profileId}`)
  }
}

async function getProfileKeeps() {
  try {
    await profilesService.getProfileKeeps(route.params.profileId)
  } catch (error) {
    Pop.toast("Could not get profile's keeps", 'error')
    console.log(`Could not get keeps for profile id: ${route.params.profileId}`)
  }
}


onMounted(() => {
  setActiveProfile()
  getProfileVaults()
  getProfileKeeps()
}
)

</script>

<template>
  <div class="about text-center">
    <div class="container w-md-75" v-if="profile">

      <div class="my-3">
        <div class="hero-section rounded rounded-3 shadow mt-5 w-md-75 mx-auto row justify-content-end align-items-end">

          <button v-if="profile?.id == account?.id"
            class="edit-button btn btn-outline-white btn-link w-auto me-3 mb-3"><i class="mdi mdi-pencil"
              data-bs-toggle="modal" data-bs-target="#update-account-modal" data-bs-dismiss="modal"
              title="Edit your account details"></i></button>
        </div>
        <div class="id-module ">
          <RoundProfilePhotoLarge :profile="profile" class="mb-3 mx-auto" />
          <h3>{{ profile.name }}</h3>
          <p>{{ vaults?.length }} Vaults | {{ keeps?.length }} Keeps</p>
        </div>
      </div>

      <div v-if="profile.id == account?.id" class="row justify-content-end">

        <AddItemFloatingButton />
      </div>


      <!-- TODO set a v-if to say that their vaults will appear here once they create some -->
      <h3 class="mt-5 fs-2">Vaults</h3>
      <div class="row">
        <div v-for="vault in vaults" :key="vault.id" class="col-6 col-md-3">
          <VaultCard :vault="vault" />
        </div>
      </div>

      <!-- TODO set a v-if to say that their keeps will appear here once they create some -->
      <h3 class="mt-5 fs-2">Keeps</h3>
      <div class="masonry my-3">
        <div v-if="keeps" class="mb-3">
          <KeepCard v-for="keep in keeps" :key="keep?.id" :keep="keep" />
        </div>
      </div>
    </div>

    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <KeepWall :keeps="keeps" />
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
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
