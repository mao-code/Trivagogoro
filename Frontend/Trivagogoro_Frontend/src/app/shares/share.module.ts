import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoadingComponent } from './loading/loading.component';
import { RestaurantInfoComponent } from './restaurant-info/restaurant-info.component';
import { SocialPostComponent } from './social-post/social-post.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    LoadingComponent,
    RestaurantInfoComponent,
    SocialPostComponent
  ],
  imports: [
    CommonModule
  ],
  exports:[
    LoadingComponent,
    RestaurantInfoComponent,
    SocialPostComponent,
    FormsModule
  ]
})
export class ShareModule { }
