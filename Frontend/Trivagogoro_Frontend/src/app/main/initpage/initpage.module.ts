import { InitpageComponent } from './initpage.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { InitpageRoutingModule } from './initpage-routing.module';


@NgModule({
  declarations: [
    InitpageComponent
  ],
  imports: [
    CommonModule,
    InitpageRoutingModule
  ]
})
export class InitpageModule { }
