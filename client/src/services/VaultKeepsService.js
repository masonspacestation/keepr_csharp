/* eslint-disable no-console */
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { api } from "./AxiosService.js"




class VaultKeepsService {
  async destroyVaultKeep(vkId) {
    const myVaultKeeps = AppState.vaultKeeps
    const vkIndex = myVaultKeeps.findIndex(keep => keep.id == vkId)
    const response = await api.delete(`api/vaultKeeps/${vkId}`)
    console.log('Destroying vaultKeep', response.data);
    if(vkIndex == -1){
      console.log('vaultKeep not found - check findIndex function on delete');
    }
    myVaultKeeps.splice(vkIndex, 1)
  }
  async getKeepsByVaultId(vaultId) {
    const response = await api.get(`api/vaults/${vaultId}/keeps`)
    const vaultKeeps = response.data.map(vkData => new Keep(vkData))
    AppState.activeVaultKeeps = vaultKeeps 
    console.log("Vault's keeps", response.data);
  }
  async createVaultKeep(vkData) {
    const response = await api.post('api/vaultkeeps', vkData)
    console.log('New vaultKeep: ', response.data);
    const newVk = new VaultKeep(response.data)

    if (AppState.activeKeep.id == newVk.keepId){AppState.vaultKeeps.push(newVk)}

    const foundKeep = AppState.keeps.find(keep => keep.id == newVk.keepId)
    if (!foundKeep) {return}
    console.log('found keep', foundKeep);

    AppState.vaultKeeps.unshift(newVk)
    return newVk
    }

  }


export const vaultKeepsService = new VaultKeepsService()