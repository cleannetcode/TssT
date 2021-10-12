import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import {environment} from "../../environments/environment";
import {BaseCollectionResponse} from "../contracts/BaseCollectionResponse";
import {AuthService} from "./auth.service";
import {map, Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class TestService {

  API_URL = environment.api;

  constructor(private http: HttpClient,
              private router: Router,
              private authService: AuthService
              ) {

    const headers = new HttpHeaders()
      .set('content-type', 'application/json');

    http.options(this.API_URL, {
      'headers': headers
    });
  }

  getTests(): Observable<BaseCollectionResponse>{
    return this.http.get<BaseCollectionResponse>(this.API_URL + "/Test/GetAll");
  }

  getTest(id: number){
    return this.http.get<BaseCollectionResponse>(this.API_URL + "/Test/Get");
  }

}
