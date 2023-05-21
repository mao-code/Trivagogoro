import { HomepageComponent } from './homepage.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomepageRoutingModule } from './homepage-routing.module';
import { ShareModule } from 'src/app/shares/share.module';


@NgModule({
  declarations: [
    HomepageComponent
  ],
  imports: [
    CommonModule,
    HomepageRoutingModule,
    ShareModule
  ]
})
export class HomepageModule { }
