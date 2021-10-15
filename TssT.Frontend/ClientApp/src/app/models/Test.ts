import {Topic} from "./Topic";

export class Test
{
  public id: number
  public Name: string
  public description: string
  public topics: Topic[] = new Array<Topic>()

  constructor(name:string = '', description: string = '') {
    this.Name = name || '';
    this.description = description || '';
  }
}
