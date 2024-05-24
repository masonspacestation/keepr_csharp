import { DBItem } from "./DBItem.js";

export class VaultKeep extends DBItem{
  constructor(data){
    super(data)
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.creatorId = data.creatorId
  }
}


/**
 * 
get my vaults:
use accountId to find vaults with creatorId = account.id

get profile. . .

createVaultKeep
bind keepId in button

dropdown of user's vaults:
  vault will be in dropdown, with vaultId as option's value

vaultKeep.keepId = keepId
vaultKeep.vaultid = vaultId




*/