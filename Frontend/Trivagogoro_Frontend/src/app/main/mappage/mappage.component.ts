import { Component } from '@angular/core';

@Component({
  selector: 'app-mappage',
  templateUrl: './mappage.component.html',
  styleUrls: ['./mappage.component.css']
})
export class MappageComponent {
  // tutorial here: https://www.c-sharpcorner.com/article/how-to-integrate-google-maps-in-angular-14-app/

  options: google.maps.MapOptions = {
    center: { lat: 24.9879, lng: 121.5774 },   // NCCU
    zoom: 17,
    mapTypeControl: false,
    fullscreenControl: false
  };

  searchKeywords: string = "";
  isSearching: boolean = false;

  constructor()
  {

  }

  search(event: KeyboardEvent)
  {
    if(event.key == "Enter")
    {
      // search with keywords (with only the restaurants in taipei i.e. in DB)
      console.log(this.searchKeywords);
    }
  }

  back()
  {
    this.isSearching = false;
    this.searchKeywords = "";
  }
}
