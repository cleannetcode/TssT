import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import {Router} from "@angular/router";

@Component({
  selector: 'app-lk',
  templateUrl: './lk.component.html',
  styleUrls: ['./lk.component.scss'],
  host: { 'class': 'lk' }
})

export class LkComponent implements AfterViewInit{

  @ViewChild('menuItems') menuItems:ElementRef | undefined;

  navMenu = [
    { route: '/lk/profile', text: 'Профиль', icon: 'face' },
    { route: '/logout', text: 'Выход', icon: 'exit_to_app' },
  ];

  currentUser = this.authService.getCurrentUser();

  constructor(private authService: AuthService, private router: Router) {

  }
  ngAfterViewInit(): void {

  }

  getCurrentRoute(){
    return this.router.url
  }

}
