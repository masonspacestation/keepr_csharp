/* eslint-disable no-console */
import { AppState } from "../AppState.js";
import { VaultKeep } from "../models/VaultKeep.js";
import { api } from "./AxiosService.js"




class VaultKeepsService {
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