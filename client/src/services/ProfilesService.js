import { AppState } from "../AppState.js";
import { Profile } from "../models/Profile.js";
import { api } from "./AxiosService.js";




class ProfilesService {
  async getProfileVaults(profileId) {
  const response = await api.get(`api/profiles/${profileId}/vaults`)
  console.log(`Vaults for ${profileId}: `, response.data);
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