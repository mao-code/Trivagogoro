import { Component, EventEmitter, Input, Output } from '@angular/core';
import { SearchRestaurantRes } from 'src/app/models/Responses/SearchRestaurantRes';

@Component({
  selector: 'app-restaurant-info',
  templateUrl: './restaurant-info.component.html',
  styleUrls: ['./restaurant-info.component.css']
})
export class RestaurantInfoComponent {
  @Input('resInfo') resInfo!: SearchRestaurantRes;

  @Output("close") closeEvent: EventEmitter<boolean>;

  constructor()
  {
    this.closeEvent = new EventEmitter<boolean>();
  }

  close()
  {
    this.closeEvent.emit(true);
  }
}
