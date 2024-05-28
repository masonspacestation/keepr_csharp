import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { Profile } from "../models/Profile.js";
import { Vault } from "../models/Vault.js";
import { api } from "./AxiosService.js";




class ProfilesService {
  async getProfileKeeps(profileId) {
    const response = await api.get(`api/profiles/${profileId}/keeps`)
    console.log(`Keeps for ${profileId}: `, response.data);
    const profileKeeps = response.data.map(keepData => new Keep(keepData))
    AppState.profileKeeps = profileKeeps
  }
  async getProfileVaults(profileId) {
  const response = await api.get(`api/profiles/${profileId}/vaults`)
  console.log(`Vaults for ${profileId}: `, response.data);
  const profileVaults = response.data.map(vaultData => new Vault(vaultData))
  AppState.profileVaults = profileVaults
  }

  async getProfileById(profileId) {
    const response = await api.get(`api/profiles/${profileId}`)
    console.log('Found profile', response.data);
    const profile = new Profile(response.data)
    return profile
  }

  async setActiveProfile(profileId) {
    AppState.activeProfile = null
    const activeProfile = await this.getProfileById(profileId)
    AppState.activeProfile = activeProfile
    console.log('Active profile: ', AppState.activeProfile);
  }

}


export const profilesService = new ProfilesService()