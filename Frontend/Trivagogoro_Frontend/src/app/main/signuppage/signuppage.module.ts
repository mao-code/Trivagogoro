import { SignuppageComponent } from './signuppage.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SignuppageRoutingModule } from './signuppage-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    SignuppageComponent
  ],
  imports: [
    CommonModule,
    SignuppageRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class SignuppageModule { }
