import { Component } from '@angular/core';

@Component({
  selector: 'app-mappage',
  templateUrl: './mappage.component.html',
  styleUrls: ['./mappage.component.css']
})
export class MappageComponent {
  // tutorial here: https://www.c-sharpcorner.com/article/how-to-integrate-google-maps-in-angular-14-app/

  // NCCU
  center: google.maps.LatLngLiteral = {
    lat: 24.9879,
    lng: 121.5774
  };
  zoom = 17;

  constructor()
  {

  }
}
