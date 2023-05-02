import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddRestaurantToFavoriteReq } from 'src/app/models/Requests/AddRestaurantToFavoriteReq';
import { ResponseData } from 'src/app/models/ResponseData';
import { GetAllFavoriteRestaurantsRes, GetAllFavoriteRestaurantsDTO } from 'src/app/models/Responses/GetAllFavoriteRestaurantsRes';
import { SearchRestaurantRes } from 'src/app/models/Responses/SearchRestaurantRes';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {
  baseURI: string;

  constructor(private http: HttpClient)
  {
    this.baseURI = `${environment.domain}/${environment.baseRoute.restaurant}`;
  }

  searchRestaurant(keywords: string): Observable<ResponseData<SearchRestaurantRes[]>>
  {
    return this.http.get<ResponseData<SearchRestaurantRes[]>>(`${this.baseURI}/search/${keywords}`);
  }

  addToFavorite(req: AddRestaurantToFavoriteReq): Observable<ResponseData<null>>
  {
    return this.http.post<ResponseData<null>>(`${this.baseURI}/favorite`, req);
  }

  getAllFavoriteRestaurants(userId: number): Observable<ResponseData<GetAllFavoriteRestaurantsRes>>
  {
    return this.http.get<ResponseData<GetAllFavoriteRestaurantsRes>>(`${this.baseURI}/favorite/all/${userId}`);
  }
}
