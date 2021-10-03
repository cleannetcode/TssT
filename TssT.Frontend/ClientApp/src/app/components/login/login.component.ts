import '@angular/compiler';
import { AfterViewInit, Component, Injectable, ElementRef, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { finalize } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})

export class LoginComponent implements AfterViewInit  {

  @ViewChild('login', {static: true}) inputLogin:ElementRef | undefined;

  isLoading: boolean = false;
  isPassFieldHide: boolean = true;

  constructor(private router: Router, private authService: AuthService, private _snackbar: MatSnackBar) {
    if (router.url === '/logout')
      this.logout();

    //if user is logged redirect to homepage
    if (this.authService.isLoggedIn())
      this.router.navigate(['/lk']);
  }
  ngAfterViewInit(): void {

    this.profileForm.controls['login'].setErrors({ 'incorrect': false });
    this.profileForm.controls['password'].setErrors({ 'incorrect': false });

    setTimeout(x => {
      this.inputLogin?.nativeElement.focus();
    })

  }

  welcomeText: string = 'Добро пожаловать в систему TssT';

  profileForm = new FormGroup({
    login: new FormControl(''),
    password: new FormControl(''),
  })

  signIn(){

    if (!this.profileForm.valid){
      return;
    }

    this.setLoading(true);

    let login = this.profileForm.controls['login'].value;
    let password = this.profileForm.controls['password'].value;

    this.authService.login(login, password)
      .pipe(
        finalize(() => this.setLoading(false))
      )
      .subscribe(next => {
          this.router.navigate(['/lk']);
        },
        error => {
          this.profileForm.setValue({login: this.profileForm.get('login').value, password:''});
          this._snackbar.open(error.message, "ok", { duration: 5 * 1000 });
        });
  }

  signUp(){
    this.router.navigate(['/lk']);
  }

  logout(){
    this.authService.logout();
    this.router.navigate(['login']);
  }

  setLoading(isLoading:boolean){
    setTimeout(()=>{
      this.isLoading = isLoading;
    })
  }

}
