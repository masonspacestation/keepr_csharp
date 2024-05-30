<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";


defineProps({ modalId: String })

</script>

<template>
  <div class="modal fade" :id="modalId" tabindex="-1" data-bs-keyboard="false" role="dialog"
    aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl mx-4 mx-sm-auto p-0"
      role="document">
      <div class="modal-content">

        <div class="modal-body p-0">
          <slot>

            Swap out modal content
          </slot>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>
.body {
  position: relative;
  flex: 1 1 auto;
}
</style>
