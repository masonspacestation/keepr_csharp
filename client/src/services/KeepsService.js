/* eslint-disable no-console */
import { computed } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";

const account = computed(()=> AppState.account)

class KeepsService{

  async destroyKeep(keepId) {
    const myKeeps = AppState.myKeeps
    const keepIndex = myKeeps.findIndex(keep => keep.id == keepId)
    const response = await api.delete(`api/keeps/${keepId}`)
    console.log('Destroying keep', response.data);
    if(keepIndex == -1){
      console.log('Keep not found - check findIndex function on delete');
    }
    myKeeps.splice(keepIndex, 1)
  }

  async createKeep(keepData) {
    const response = await api.post('api/keeps', keepData)
    console.log('Creating new keep', response.data);
    const newKeep = new Keep(response.data)
    AppState.keeps.unshift(newKeep)
    return newKeep
  }
  async getKeepById(keepId) {
    const response = await api.get(`api/keeps/${keepId}`)
    console.log("Found keep: ", response.data);
    AppState.activeKeep = new Keep(response.data)
    console.log('activeKeep', AppState.activeKeep);
  }

// async setActiveKeep(keepId){
//   const activeKeep = await this.getKeepById(keepId)
//   AppState.activeKeep = activeKeep

// const foundKeep = AppState.keeps.find(keep => keep.id == activeKeep.id)
// if (!foundKeep) {return}
// foundKeep.views++
//   console.log('activeKeep', AppState.activeKeep);
// }

  async getAllKeeps() {
    const response = await api.get('api/keeps')
    console.log("keeeeps'r here", response.data);
    const keeps = response.data.map(keepData => new Keep(keepData))
    AppState.keeps = keeps
  }


}


export const keepsService = new KeepsService()