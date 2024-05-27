import { AppState } from "../AppState.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { api } from "./AxiosService.js"




class VaultKeepsService {
  async createVaultKeep(vkData) {
    const response = await api.post('api/vaultkeeps', vkData)
    console.log('New vaultKeep: ', response.data);
const newVk = new VaultKeep(response.data)
AppState.myKeeps.unshift(newVk)
return newVk
  }

}


export const vaultKeepsService = new VaultKeepsService()