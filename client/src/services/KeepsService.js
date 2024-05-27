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
    const keep = new Keep(response.data)
    return keep    
  }

async setActiveKeep(keepId){
  const activeKeep = await this.getKeepById(keepId)
  AppState.activeKeep = activeKeep
  console.log('activeKeep', AppState.activeKeep);
  // return activeKeep
}

  async getAllKeeps() {
    const response = await api.get('api/keeps')
    console.log("keeeeps'r here", response.data);
    const keeps = response.data.map(keepData => new Keep(keepData))
    AppState.keeps = keeps
  }


}


export const keepsService = new KeepsService()