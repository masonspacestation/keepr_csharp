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
  <div class="d-flex justify-content-center">
    <img @click="getProfileById(profile.id)" class="profile-pic shadow" :src="profile?.picture"
      :alt="`Profile picture for ${profile.name}`" :title="`Profile picture for ${profile.name}`">
  </div>
</template>


<style lang="scss" scoped>
.profile-pic {
  height: 10vh;
  aspect-ratio: 1/1;
  border-radius: 50%;
}
</style>