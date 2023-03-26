import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        redirectTo: 'signin',
        pathMatch: 'full'
      },
      {
        path: 'user',
        loadChildren: () => import('./userpage/userpage.module').then(m => m.UserpageModule),
      },
      {
        path: 'signin',
        loadChildren: () => import('./signinpage/signinpage.module').then(m => m.SigninpageModule),
      },
      {
        path: 'signup',
        loadChildren: () => import('./signuppage/signuppage.module').then(m => m.SignuppageModule),
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
