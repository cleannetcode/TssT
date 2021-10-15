import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LoginComponent } from './components/login/login.component';
import { LkComponent } from './components/lk/lk.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { LkProfileComponent } from './components/lk-profile/lk-profile.component';
import { TestCreateComponent } from './components/test.create/test.create.component';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LoginComponent },
  { path: 'lk', redirectTo: 'lk/profile', pathMatch: 'full' },
  { path: 'lk', component: LkComponent,
    children:[
      { path: 'profile', component: LkProfileComponent},//canActivate: [AuthGuard]
      {
        path: 'test',
        children: [
          {
            path: 'create', component: TestCreateComponent
          }
        ]
      },
    ]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
