import { AppComponent } from './app.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: AppComponent,
    children:[
      {path: '', redirectTo: 'init', pathMatch: 'full'},
      {
        path: 'main',
        loadChildren: () => import('./main/main.module').then(m => m.MainModule)
      },
      {
        path: 'init',
        loadChildren: () => import('./main/initpage/initpage.module').then(m => m.InitpageModule)
      },
      {
        path: 'signin',
        loadChildren: () => import('./main/signinpage/signinpage.module').then(m => m.SigninpageModule),
      },
      {
        path: 'signup',
        loadChildren: () => import('./main/signuppage/signuppage.module').then(m => m.SignuppageModule),
      },
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
