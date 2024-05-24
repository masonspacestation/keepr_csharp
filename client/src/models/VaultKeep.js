import { DBItem } from "./DBItem.js";

export class VaultKeep extends DBItem{
  constructor(data){
    super(data)
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.creatorId = data.creatorId
  }
}