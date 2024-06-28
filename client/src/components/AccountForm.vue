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
  <h5 class="p-3 pb-2">Update your account details</h5>
  <hr>
  <form @submit.prevent="saveAccount()">
    <div class="col-12 p-3">
      <div class="row">
        <div class="col-12 col-lg-4 mb-3">
          <label for="account-name">Name</label>
          <input placeholder="Name . . ." v-model="accountData.name" class="form-control" type="text" id="account-name"
            required maxlength="100">
        </div>

        <div class="mb-3 col-12 col-lg-4">
          <label for="account-picture">Profile Picture</label>
          <input placeholder="Image url . . ." v-model="accountData.picture" class="form-control" type="url"
            id="account-picture" maxlength="1000">
        </div>

        <div class="mb-3 col-12 col-lg-4">
          <label for="account-cover-image">Cover Image</label>
          <input placeholder="Image url . . ." v-model="accountData.coverImg" class="form-control" type="url"
            id="account-cover-image" maxlength="1000">
        </div>

        <div class="modal-footer col-12 d-flex align-items-center justify-content-end">
          <button type="submit" class="btn btn-primary w-25" data-bs-dismiss="modal">Save</button>

        </div>
      </div>
    </div>
  </form>

</template>


<style lang="scss" scoped></style>