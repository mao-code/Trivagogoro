import { Component, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { RestaurantService } from 'src/app/services/restaurant/restaurant.service';

@Component({
  selector: 'app-mappage',
  templateUrl: './mappage.component.html',
  styleUrls: ['./mappage.component.css']
})
export class MappageComponent implements OnDestroy {
  // tutorial here: https://www.c-sharpcorner.com/article/how-to-integrate-google-maps-in-angular-14-app/
  destroy$: Subject<null>;
  isLoading: boolean;

  options: google.maps.MapOptions = {
    center: { lat: 24.9879, lng: 121.5774 },   // NCCU
    zoom: 17,
    mapTypeControl: false,
    fullscreenControl: false
  };

  searchKeywords: string = "";
  isSearching: boolean = false;

  constructor(private restaurantService: RestaurantService)
  {
    this.isLoading = false;
    this.destroy$ = new Subject<null>();
  }

  search(event: KeyboardEvent)
  {
    if(event.key == "Enter")
    {
      // search with keywords (with only the restaurants in taipei i.e. in DB)
      this.isLoading = true;
      this.restaurantService.searchRestaurant(this.searchKeywords)
      .pipe(
        takeUntil(this.destroy$)
      ).subscribe(res => {
        console.log(res);
        this.isLoading = false;
      });
    }
  }

  back()
  {
    this.isSearching = false;
    this.searchKeywords = "";
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
