import { SigninpageComponent } from './signinpage.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SigninpageRoutingModule } from './signinpage-routing.module';


@NgModule({
  declarations: [
    SigninpageComponent
  ],
  imports: [
    CommonModule,
    SigninpageRoutingModule
  ]
})
export class SigninpageModule { }
