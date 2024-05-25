<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";

const keep = computed(() => AppState.activeKeep)
</script>


<template>
  <KeepModal :keep="keep" />
</template>


<style lang="scss" scoped></style>