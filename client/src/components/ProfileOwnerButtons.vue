<script setup>
import { Profile } from "../models/Profile.js";

const owner = defineProps({ profile: { type: Profile, required: true } })
</script>


<template>
  <div class="container text-end">

    <span class="dropdown bg-info">
      <button type="button" class="btn btn-primary dropdown-toggle fs-6" data-bs-toggle="dropdown" aria-expanded="false"
        data-bs-auto-close="outside"><i class="mdi mdi-plus fw-bold"></i>
      </button>
      <ul class="dropdown-menu dropdown-menu-end container justify-contents-between align-items-center"
        aria-labelledby="dropdownMenuButton1">
        <div class="row">
          <div class=" col-6 p-2 d-inline"> <button class="btn btn-primary" data-bs-toggle="modal"
              data-bs-target="#create-vault-modal">Add Vault</button></div>
          <div class=" col-6 p-2 d-inline"> <button class="btn btn-outline-primary" data-bs-toggle="modal"
              data-bs-target="#create-keep-modal">Add Keep</button></div>
        </div>
      </ul>
    </span>




    <div class="row justify-content-end align-items-center">

      <button class="btn btn-outline-secondary w-auto opacity-50"><i class="mdi mdi-pencil" data-bs-toggle="modal"
          data-bs-target="#update-account-modal" data-bs-dismiss="modal"></i>
      </button>
    </div>
    <div class="col-1">
    </div>
  </div>

</template>


<style lang="scss" scoped></style>