import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PostingRoutingModule } from './posting-routing.module';
import { PostingComponent } from './posting.component';
import { ShareModule } from 'src/app/shares/share.module';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    PostingComponent
  ],
  imports: [
    CommonModule,
    PostingRoutingModule,
    ShareModule,
    FormsModule
  ]
})
export class PostingModule { }
