<script setup>
import { Profile } from "../models/Profile.js";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";

const props = defineProps({ profile: { type: Profile, required: true } })

async function getProfileById(profileId) {
  try {
    const profile = await accountService.getAccount
  } catch (error) {
    Pop.toast(`Could not get profile for `, props.profile.name)
  }
}

</script>


<template>
  <div class="d-flex justify-content-center profile-pic shadow">
    <img @click="getProfileById(profile.id)" role="button" class="" :src="profile?.picture"
      :alt="`Profile picture for ${profile.name}`" :title="`Profile picture for ${profile.name}`">
  </div>
</template>


<style lang="scss" scoped>
.profile-pic {
  height: 10vh;
  aspect-ratio: 1/1;
  border-radius: 50%;
  overflow: hidden;

  img {
    width: 100%;
    object-fit: cover;
    object-position: center;
  }
}
</style>