import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from './loading/loading.component';
import { RestaurantInfoComponent } from './restaurant-info/restaurant-info.component';

@NgModule({
  declarations: [
    LoadingComponent,
    RestaurantInfoComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    LoadingComponent,
    RestaurantInfoComponent
  ]
})
export class ShareModule { }
