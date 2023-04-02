import { FavoriteComponent } from './favorite.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FavoritepageRoutingModule } from './favoritepage-routing.module';


@NgModule({
  declarations: [
    FavoriteComponent
  ],
  imports: [
    CommonModule,
    FavoritepageRoutingModule
  ]
})
export class FavoritepageModule { }
