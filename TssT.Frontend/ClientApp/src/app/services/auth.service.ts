import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/internal/Observable';
import { map } from 'rxjs';
import {environment} from "../../environments/environment";
import {User} from "../models/User";
import {GetTokenResponse} from "../contracts/GetTokenResponse";

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  API_URL = environment.api;

  constructor(private http: HttpClient, private router: Router) {

    const headers = new HttpHeaders()
      .set('content-type', 'application/json');

    http.options(this.API_URL, {
      'headers': headers
    });
  }

  isLoggedIn() {
    return !!this.getCurrentUser();
  }

  login(login: string, password: string): Observable<boolean> {

    return this.http.post<GetTokenResponse>(this.API_URL+"/Auth/GetToken", {name: login, password: password})
      .pipe(map(r => {
        let user = JSON.stringify(new User(login, r.token));
        localStorage.setItem('currentUser', user);
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
