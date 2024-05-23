import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { api } from "./AxiosService.js";



class KeepsService{
  async getAllKeeps() {
    const response = await api.get('api/keeps')
    console.log("keeeeps'r here", response.data);
    const keeps = response.data.map(keepData => new Keep(keepData))
    AppState.keeps = keeps
  }


}


export const keepsService = new KeepsService;