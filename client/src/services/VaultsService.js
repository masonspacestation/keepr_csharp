import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"

const myVaults = computed(()=> AppState.myVaults)


class VaultsService{
  createVault(value) {
    throw new Error("Method not implemented.");
  }
  async destroyVault(vaultId) {
    const myVaults = AppState.myVaults
    const indexToDestroy = myVaults.findIndex(vault => vault.id == vaultId)
    const response = await api.delete(`api/vaults/${vaultId}`)
    console.log('Vault to delete', response.data);
    if(indexToDestroy == -1) throw new Error('Vault not found - check findIndex function on delete');
    myVaults.splice(indexToDestroy, 1)

  }
  async getMyVaults() {
  const response = await api.get('api/vaults')
}
async getVaultById(vaultId){
    const response = await api.get(`api/vaults/${vaultId}`)
    console.log('looking for vault', response.data);
    const vault = new Vault(response.data)
      return vault
}

async setActiveVault(vaultId){
  AppState.activeVault = null
  const activeVault = await this.getVaultById(vaultId)
  AppState.activeVault = activeVault
  console.log('Active Vault: ', AppState.activeVault);
}

}

export const vaultsService = new VaultsService()