/* eslint-disable no-console */
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";



class KeepsService{
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
    const foundKeep = AppState.keeps.find(keep => keep.id == AppState.activeKeep.id)
if (!foundKeep) {return}
foundKeep.views++
    AppState.activeKeep.views++
    console.log('activeKeep', AppState.activeKeep);
    // return keep    
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