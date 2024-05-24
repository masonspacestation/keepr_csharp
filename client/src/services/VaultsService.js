import { AppState } from "../AppState.js";
import { Vault } from "../models/Vault.js";
import { logger } from "../utils/Logger.js"
import Pop from "../utils/Pop.js"
import { api } from "./AxiosService.js"




class VaultsService{
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
  const activeVault = await this.getVaultById(vaultId)
  AppState.activeVault = activeVault
  console.log('Active Vault: ', AppState.activeVault);
}

}

export const vaultsService = new VaultsService()