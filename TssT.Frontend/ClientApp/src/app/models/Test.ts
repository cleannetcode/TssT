﻿export class Test
{
  public id: number
  public name: string
  public description: string

  constructor(name:string = "", description: string = "") {
    this.name = name;
    this.description = description;
  }
}
