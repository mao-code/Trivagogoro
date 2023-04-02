import { ExploreComponent } from './explore.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ExplorepageRoutingModule } from './explorepage-routing.module';


@NgModule({
  declarations: [
    ExploreComponent
  ],
  imports: [
    CommonModule,
    ExplorepageRoutingModule
  ]
})
export class ExplorepageModule { }
