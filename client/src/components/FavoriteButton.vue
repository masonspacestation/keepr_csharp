<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import Pop from "../utils/Pop.js";
import { keepsService } from "../services/KeepsService.js";

// const props = defineProps({ recipe: { type: Recipe, required: true } })

const keepData = {}
const account = computed(() => AppState.account)
const keep = computed(() => AppState.activeKeep)
// const favorite = computed(() => AppState.favoritedRecipes.some(favorite => favorite.accountId == account?.value.id))
const keeps = computed(() => AppState.keeps)

async function createKeep(keepId) {
  try {
    keepData.recipeId = AppState.activeKeep.id
    await keepsService.createKeep(keepData)
  } catch (error) {
    Pop.toast("Could not add to your favorites", "error")
    console.error("Error adding to favorites", error)
  }
  Pop.toast('Added Favorite!', 'success')
}

</script>


<template>
  <i v-if="keeps.some(keep => keep.id == keep.id)" role="button" @click.prevent="createKeep(keep.id)"
    class="mdi mdi-heart opacity-75 mt-1 me-2 fs-3 text-end"></i>
  <i v-else role="button" @click.prevent="createKeep(keep.id)"
    class="mdi mdi-heart-outline opacity-75 mt-1 me-2 fs-3 text-end"></i>
</template>


<style lang="scss" scoped></style>