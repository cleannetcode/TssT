import { Component, OnInit } from '@angular/core';
import {Test} from "../../models/Test";
import {AuthService} from "../../services/auth.service";
import {Router} from "@angular/router";
import {TestService} from "../../services/test.service";

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

  constructor(private router: Router,
              private authService: AuthService,
              private testService: TestService) {

    let isLogged = authService.isLoggedIn();

    if (!isLogged)
      router.navigate(['/logout']);
  }

  ngOnInit() {
    this.getTests();
  }

  getTests(){



    this.tests.completed = [
      new Test(".NET FullStack" ),
      new Test(".NET Junior"),
    ];

    this.testService.getTests().subscribe(response=>{

      if (response.totalCount > 0){
        this.tests.available = response.items;
      }

    });
  }

}
