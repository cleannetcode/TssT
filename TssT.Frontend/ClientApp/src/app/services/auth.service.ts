import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  API_URL = "http://localhost:4300";

  constructor(private http: HttpClient, private router: Router) {
  }

  isLoggedIn() {
    return !!this.getCurrentUser();
  }

  login(login: string, password: string): Observable<boolean> {
    return this.http.post(this.API_URL, {login: login, password: password}, {})
      .pipe(map(user => {
          localStorage.setItem('currentUser', JSON.stringify(user));
          return true;
      }));
  }

  logout(){
    localStorage.removeItem('currentUser');
  }

  getCurrentUser(){
    return JSON.parse(localStorage.getItem('currentUser'));
  }

}
