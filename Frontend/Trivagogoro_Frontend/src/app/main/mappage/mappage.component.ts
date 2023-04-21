import { AnimateTimings } from '@angular/animations';
import { Component, OnDestroy } from '@angular/core';
import { Subject, catchError, takeUntil, throwError } from 'rxjs';
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

  // map settings
  options: google.maps.MapOptions = {
    // center: { lat: 24.9879, lng: 121.5774 },   // NCCU
    // zoom: 16,
    mapTypeControl: false,
    fullscreenControl: false
  };

  center: any = { lat: 24.9879, lng: 121.5774 } // NCCU
  zoom: number = 16;

  // marker settings
  markerOptions: google.maps.MarkerOptions = {
    draggable: false
  };
  markerPositions: google.maps.LatLngLiteral[] = [];

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
        takeUntil(this.destroy$),
        catchError(err => {
          alert(err.error.message);
          return throwError(()=>err);
        })
      ).subscribe(res => {
        console.log(res);
        this.isLoading = false;

        // mark location
        this.markerPositions = [];
        for(let loc of res.data!)
        {
          this.addMarker(loc.lat, loc.lng);
        }

        // rezoom and recenter
        this.zoom = 13;
        this.center = this.markerPositions[Math.floor(Math.random()*this.markerPositions.length)]

        alert(`成功搜尋${res.data?.length}個結果！`)
      });
    }
  }

  addMarker(lat: number, lng: number)
  {
    this.markerPositions.push({
      lat: lat,
      lng: lng
    });
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
