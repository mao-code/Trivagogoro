import { AddRestaurantToFavoriteReq } from 'src/app/models/Requests/AddRestaurantToFavoriteReq';
import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { SearchRestaurantRes } from 'src/app/models/Responses/SearchRestaurantRes';
import { RestaurantService } from 'src/app/services/restaurant/restaurant.service';
import { UserService } from 'src/app/services/user/user.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-restaurant-info',
  templateUrl: './restaurant-info.component.html',
  styleUrls: ['./restaurant-info.component.css']
})
export class RestaurantInfoComponent implements OnDestroy, OnInit {
  @Input('resInfo') resInfo!: SearchRestaurantRes;

  @Output("close") closeEvent: EventEmitter<boolean>;
  @Output("spanOrclose") spanEvent: EventEmitter<boolean>;
  isSpanOrClose: boolean = false;

  isFav: boolean = false;

  destroy$: Subject<null>;

  constructor(
    private _userService: UserService,
    private _restaurantService: RestaurantService
  )
  {
    this.closeEvent = new EventEmitter<boolean>();
    this.spanEvent = new EventEmitter<boolean>();
    this.destroy$ = new Subject<null>();
  }

  ngOnInit(): void {
    if(this.resInfo.favId != null)
    {
      this.isFav = true;
    }
  }

  addToFavorite(rating: number)
  {
    var req: AddRestaurantToFavoriteReq = {
      userId: this._userService.getUserId(),
      restaurantId: this.resInfo.id,
      rating: rating
    };

    this._restaurantService.addToFavorite(req)
    .pipe(
      takeUntil(this.destroy$)
    )
    .subscribe(res => {
      this.isFav = !this.isFav;
      alert(res.message);
    });
  }

  close()
  {
    this.closeEvent.emit(true);
  }

  spanOrClose()
  {
    this.isSpanOrClose = !this.isSpanOrClose;
    this.spanEvent.emit(this.isSpanOrClose);
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
