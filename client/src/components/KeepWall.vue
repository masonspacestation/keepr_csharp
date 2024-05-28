<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";

const keeps = computed(() => AppState.keeps)

// defineProps({ keep: { type: Keep, required: true } })
</script>


<template>
  <div class="row">
    <div class="masonry col-12">
      <div class="p-2" v-for="keep in keeps" :key="keep.id">
        <KeepCard :keep="keep" />
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.masonry {
  columns: 4 175px;
  column-gap: 1rem;
}
</style>