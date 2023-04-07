import { FormsModule } from '@angular/forms';
import { ShareModule } from './../../shares/share.module';
import { MappageComponent } from './mappage.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MappageRoutingModule } from './mappage-routing.module';
import { GoogleMapsModule } from '@angular/google-maps';


@NgModule({
  declarations: [
    MappageComponent
  ],
  imports: [
    CommonModule,
    MappageRoutingModule,
    FormsModule,
    GoogleMapsModule,
    ShareModule
  ]
})
export class MappageModule { }
