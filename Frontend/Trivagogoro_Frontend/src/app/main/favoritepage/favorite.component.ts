import { GetAllFavoriteRestaurantsDTO } from 'src/app/models/Responses/GetAllFavoriteRestaurantsRes';
import { Component } from '@angular/core';
import { RestaurantService } from 'src/app/services/restaurant/restaurant.service';
import { UserService } from 'src/app/services/user/user.service';
import { Subject, take, takeUntil } from 'rxjs';

@Component({
  selector: 'app-favorite',
  templateUrl: './favorite.component.html',
  styleUrls: ['./favorite.component.css']
})
export class FavoriteComponent {
  activeType: any = {
    'fav': true,
    'trip': false
  }
  destroy$: Subject<null>;

  favRestaurants: GetAllFavoriteRestaurantsDTO[] = [];

  constructor(
    private userService: UserService,
    private favService: RestaurantService
  )
  {
    this.destroy$ = new Subject<null>();

    favService.getAllFavoriteRestaurants(userService.getUserId())
    .pipe(
      takeUntil(this.destroy$)
    ).subscribe(res => {
      this.favRestaurants = res.data!.favoriteRestaurants;
      console.log(this.favRestaurants);
    });
  }

  changeType(type: string)
  {
    for(var k in this.activeType)
    {
      this.activeType[k] = false;
      if(k == type)
      {
        this.activeType[k] = true;
      }
    }
  }
}
