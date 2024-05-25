<script setup>
import { computed } from "vue";
import { AppState } from "../AppState.js";

const keep = computed(() => AppState.activeKeep)
defineProps({ modalId: String })

</script>

<template>
  <div class="modal fade" :id="modalId" tabindex="-1" data-bs-keyboard="false" role="dialog"
    aria-labelledby="modalTitleId" aria-hidden="true">
    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-xl w-lg-75 mx-4 mx-sm-auto"
      role="document">
      <div class="modal-content">

        <div class="body">
          <slot>
            <KeepModal :keep="keep" />
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
