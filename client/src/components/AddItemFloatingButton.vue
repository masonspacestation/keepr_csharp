<script setup>

</script>


<template>
  <section class="p-0 m-0 text-center text-lg-end">

    <div class="dropdown container">
      <button type="button" class="btn btn-primary dropdown-toggle fs-6" data-bs-toggle="dropdown" aria-expanded="false"
        data-bs-auto-close="outside" title="Create a new vault or keep">
        Create
      </button>
      <ul class="dropdown-menu dropdown-menu-end row justify-contents-between align-items-center"
        aria-labelledby="dropdownMenuButton1">
        <div class=" col-6 p-2 d-inline"> <button class="btn btn-primary" data-bs-toggle="modal"
            data-bs-target="#create-vault-modal" title="Create a new vault">Add Vault</button></div>
        <div class=" col-6 p-2 d-inline"> <button class="btn btn-outline-primary" data-bs-toggle="modal"
            data-bs-target="#create-keep-modal" title="Create a new keep">Add Keep</button></div>
      </ul>
    </div>

  </section>

</template>

<style lang="scss" scoped></style>
