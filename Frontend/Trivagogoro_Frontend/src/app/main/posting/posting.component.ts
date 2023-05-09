import { PostRestaurantReq } from './../../models/Requests/PostRestaurantReq';
import { Location } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { GetAllFavoriteRestaurantsDTO } from 'src/app/models/Responses/GetAllFavoriteRestaurantsRes';
import { RestaurantService } from 'src/app/services/restaurant/restaurant.service';
import { SocialService } from 'src/app/services/social/social.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-posting',
  templateUrl: './posting.component.html',
  styleUrls: ['./posting.component.css']
})
export class PostingComponent implements OnDestroy, OnInit{
  activeImage: string = "";
  favList: GetAllFavoriteRestaurantsDTO[] = []

  destroy$: Subject<null> = new Subject<null>();

  isLoading: boolean = false;
  isNext: boolean = false;

  description: string = "";

  constructor(
    private location: Location,
    private router: Router,
    private restService: RestaurantService,
    private userService: UserService,
    private socialService: SocialService
  )
  {

  }

  ngOnInit(): void {
    this.isLoading = true;
    this.restService.getAllFavoriteRestaurants(this.userService.getUserId())
    .pipe(
      takeUntil(this.destroy$)
    ).subscribe(res => {
      this.favList = res.data ? res.data.favoriteRestaurants : [];
      this.activeImage = this.favList[0].images[0];
      this.isLoading = false;
    });
  }

  clickActiveImage(img: string)
  {
    this.activeImage = img;
  }

  next()
  {
    this.isNext = true;
  }

  post()
  {
    var postRest = this.favList.find(_ => _.images[0] == this.activeImage);
    if(postRest)
    {
      var req: PostRestaurantReq = {
        title: postRest!.resName,
        description: this.description,
        type: 1,
        sourceId: postRest.favId,
        userId: this.userService.getUserId()
      }

      this.socialService.postingRestaurantPost(req)
      .pipe(
        takeUntil(this.destroy$)
      ).subscribe(res => {
        console.log(res);

        if(res.isSuccess)
        {
          alert("Posting successfully!");
          this.router.navigate(['main/user']);
        }
      });
    }
  }

  back()
  {
    this.location.back();
  }

  ngOnDestroy(): void {
      this.destroy$.next(null);
      this.destroy$.complete();
  }
}
