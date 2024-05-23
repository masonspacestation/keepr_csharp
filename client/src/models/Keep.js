import { DBItem } from "./DBItem.js";

export class Keep extends DBItem{
  constructor(data){
  super(data)
  this.name = data.name
  this.description = data.description
  this.img = data.img
  this.views = data.views
  this.creatorId = data.creatorId
}
}