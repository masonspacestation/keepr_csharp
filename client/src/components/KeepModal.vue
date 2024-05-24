<script setup>
import { Keep } from "../models/Keep.js";
import RoundProfilePhoto from "./RoundProfilePhoto.vue";

defineProps({ keep: { type: Keep, required: true } })

</script>


<template>
  <div class="container rounded rounded-2 shadow mb-3">
    <div class="row p-0">
      <div class="col-6 p-0">
        <!-- hiya 👋 -->
        <img :src="keep.img" alt="" class="rounded-start-2">
      </div>
      <div class="col-6 d-flex flex-column justify-content-between align-items-center">
        <h3>{{ keep.name }}</h3>
        <p>{{ keep.description }}</p>
        <div class="row w-100 align-items-center">
          <div class="col-6 text-center">
            <button class="btn btn-primary w-50">vault</button>
          </div>
          <div class="col-6 ms-auto">
            <small class="w-auto">{{ keep.creator.name }}</small>
            <div class="">
              <RoundProfilePhoto :profile="keep.creator" class="" />
            </div>

          </div>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
img {
  width: 100%;
  object-fit: cover;
  object-position: center;
}
</style>