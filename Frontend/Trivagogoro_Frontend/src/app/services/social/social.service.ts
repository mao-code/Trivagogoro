import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PostRestaurantReq } from 'src/app/models/Requests/PostRestaurantReq';
import { ResponseData } from 'src/app/models/ResponseData';
import { GetFollowPostsDTO, GetFollowPostsRes } from 'src/app/models/Responses/GetFollowedPostDTO';
import { GetPostedPostRestRes } from 'src/app/models/Responses/GetPostedPostRestRes';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SocialService {
  baseURI: string;

  constructor(private http: HttpClient) {
    this.baseURI = `${environment.domain}/${environment.baseRoute.social}`;
  }

  postingRestaurantPost(req: PostRestaurantReq): Observable<ResponseData<null>>
  {
    return this.http.post<ResponseData<null>>(`${this.baseURI}/post`, req);
  }

  getPostedPostRest(userId: number): Observable<ResponseData<GetPostedPostRestRes>>
  {
    return this.http.get<ResponseData<GetPostedPostRestRes>>(`${this.baseURI}/posted/${userId}`)
  }

  followUser(from: number, to: number, value: boolean): Observable<ResponseData<null>>
  {
    return this.http.post<ResponseData<null>>(`${this.baseURI}/follow`, {
      fromUserId: from,
      toUserId: to,
      followValue: value
    });
  }

  getFollowedPosts(userId: number): Observable<ResponseData<GetFollowPostsRes>>
  {
    return this.http.get<ResponseData<GetFollowPostsRes>>(`${this.baseURI}/followed/post/${userId}`);
  }
}
