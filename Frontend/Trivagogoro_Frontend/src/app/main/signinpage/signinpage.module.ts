import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
    SigninpageRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SigninpageModule { }
