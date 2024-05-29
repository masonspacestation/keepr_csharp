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
const vaults = computed(() => AppState.profileVaults)


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
    <div class="container" v-if="profile">


      <div class="my-3">
        <RoundProfilePhoto :profile="profile" />
        <!-- <img class="rounded" :src="account.picture" alt="" /> -->
        <h1>{{ profile.name }}</h1>
        <p>{{ vaults?.length }} Vaults | XX Keeps</p>
      </div>
      <div v-if="profile.id = account?.id" class="row justify-content-end">
        <AddItemFloatingButton />
      </div>

      <!-- TODO set a v-if to say that their vaults will appear here once they create some -->
      <h3>Vaults</h3>
      <div class="row">
        <div v-for="vault in vaults" :key="vault.id" class="col-6 col-md-3">
          <VaultCard :vault="vault" />
        </div>
      </div>

      <!-- TODO set a v-if to say that their keeps will appear here once they create some -->
      <h3>Keeps</h3>
      <div class="row">
        <div class="masonry col-12">
          <!-- <div class="p-2" v-for="keep in keeps" :key="keep.id">
            <KeepCard :keep="keep" />
          </div> -->
          <div v-for="keep in keeps" :key="keep?.id" class="col-12 col-md-6 col-lg-4 py-3 px-4 masonry">
            <KeepCard :keep="keep" />
          </div>
        </div>
      </div>
    </div>
    <div v-else>
      <h1>Loading... <i class="mdi mdi-loading mdi-spin"></i></h1>
    </div>
  </div>
  <ModalWrapper modalId="create-vault-modal">
    <CreateVaultForm />
  </ModalWrapper>
  <ModalWrapper modalId="create-keep-modal">
    <CreateKeepForm />
  </ModalWrapper>
</template>

<style scoped lang="scss">
img {
  max-width: 100px;
}
</style>
