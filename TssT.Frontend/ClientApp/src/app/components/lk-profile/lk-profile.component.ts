import { Component, OnInit } from '@angular/core';
import {Test} from "../../models/Test";

@Component({
  selector: 'app-lk-profile',
  templateUrl: './lk-profile.component.html',
  styleUrls: ['./lk-profile.component.scss']
})
export class LkProfileComponent implements OnInit {

  tests = {
    uncompleted: new Array<Test>(),
    completed: new Array<Test>(),
    available: new Array<Test>(),
  }

  constructor() {

  }

  ngOnInit() {
    this.getTests();
  }

  getTests(){

    this.tests.completed = [
      new Test(".NET FullStack" ),
      new Test(".NET Junior"),
    ];

    this.tests.available = [
      new Test(".NET Senior"),
      new Test(".NET Middle"),
    ];

  }

}
