<!-- eslint-disable no-console -->
<script setup>
import { computed, onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { accountService } from "../services/AccountService.js";
import Pop from "../utils/Pop.js";
import { Modal } from "bootstrap";
import { Account } from "../models/Account.js";

const account = computed(() => AppState.account)


const accountData = ref({
  name: '',
  picture: '',
  coverImg: '',
})

watchEffect(() => {
  accountData.value = { ...AppState.account }
})

// function closeModal() {
//   Modal.getOrCreateInstance('#staticBackdrop').hide()
// }

async function saveAccount() {
  try {
    await accountService.updateAccount(accountData.value)
    Modal.getOrCreateInstance('#update-account-modal').hide()
    Pop.toast("Account saved!", 'success')
  } catch (error) {
    Pop.toast("Could not save account changes", 'error')
    console.error(error)
  }
}

</script>


<template>

  <!-- <div class="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1"
    aria-labelledby="staticBackdropLabel" aria-hidden="true">
    <div class="modal-dialog modal-lg modal-dialog-scrollable">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-5" id="staticBackdropLabel">Edit Profile Details</h1>
          <button type="button" class="btn bg-none text-secondary opacity-50" data-bs-dismiss="modal"
            aria-label="Close and discard changes">Cancel</button>
        </div>
        <div class="modal-body">
          <div class="container-fluid"> -->


  <h5 class="p-3 pb-2">Update your account details</h5>
  <hr>
  <form @submit.prevent="saveAccount()">
    <div class="col-12 p-3">
      <div class="row">
        <div class="col-4 mb-3">
          <label for="account-name">Name</label>
          <input placeholder="Name . . ." v-model="accountData.name" class="form-control" type="text" id="account-name"
            required maxlength="100">
        </div>

        <div class="mb-3 col-4">
          <label for="account-picture">Profile Picture</label>
          <input placeholder="Image url . . ." v-model="accountData.picture" class="form-control" type="url"
            id="account-picture" maxlength="1000">
        </div>

        <div class="mb-3 col-4">
          <label for="account-cover-image">Cover Image</label>
          <input placeholder="Image url . . ." v-model="accountData.coverImg" class="form-control" type="url"
            id="account-cover-image" maxlength="1000">
        </div>

        <!-- <div class="mb-3 col-4">
          <label for="account-email">Email Address</label>
          <input placeholder="email address" v-model="accountData.email" class="form-control" type="email"
            id="account-email" maxlength="500">
        </div> -->

        <div class="modal-footer col-12 d-flex align-items-center justify-content-end">
          <button type="submit" class="btn btn-primary w-25" data-bs-dismiss="modal">Save</button>

        </div>
      </div>
    </div>
  </form>

</template>


<style lang="scss" scoped></style>