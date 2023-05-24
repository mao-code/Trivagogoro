import { SocialService } from './../../services/social/social.service';
import { Component, OnDestroy } from '@angular/core';
import { Subject, mergeMap, takeUntil } from 'rxjs';
import { User } from 'src/app/models/Entities/User';
import { GetFollowPostsDTO } from 'src/app/models/Responses/GetFollowedPostDTO';
import { GetPostedPostRestDTO } from 'src/app/models/Responses/GetPostedPostRestRes';
import { SearchUserDTO } from 'src/app/models/Responses/SearchUserDTO';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnDestroy{
  allPosts: (GetPostedPostRestDTO|GetFollowPostsDTO)[] = [];

  myPosts: GetPostedPostRestDTO[] = [];
  followingPosts: GetFollowPostsDTO[] = [];

  destroy$: Subject<null>;

  tmpUser?: User;

  keyword: string = "";
  searchUserResults: SearchUserDTO[] = [];

  isSearching: boolean = false;

  constructor(
    private userService: UserService,
    private socialService: SocialService
  )
  {
    this.destroy$ = new Subject<null>();

    userService.getUserInfo(userService.getUserId())
    .pipe(
      takeUntil(this.destroy$)
    )
    .subscribe(res => {
      this.tmpUser = res.data;
    });

    socialService.getPostedPostRest(userService.getUserId())
    .pipe(
      takeUntil(this.destroy$),
      mergeMap(_ => {
        if(_.data?.postedPostRest)
        {
          this.myPosts = _.data.postedPostRest;
        }
        return socialService.getFollowedPosts(userService.getUserId());
      })
    )
    .subscribe(res => {
      this.followingPosts = res.data!.followedPostDTOs;
      this.mergeAndShufflePosts();
      console.log(this.allPosts);
    });


  }

  searchUser(event: KeyboardEvent)
  {
    if(event.key === "Enter")
    {
      // do search user
      this.userService.searchUser(this.keyword, this.userService.getUserId())
      .pipe(
        takeUntil(this.destroy$)
      )
      .subscribe(res => {
        console.log(res);
        this.searchUserResults = res.data!.filter(_ => _.userId != this.userService.getUserId());
      });
    }
  }

  follow(userId: number, isFollow: boolean)
  {
    var from = this.userService.getUserId();
    var to = userId;
    var followValue = !isFollow;

    this.socialService.followUser(from, to, followValue)
    .pipe(
      takeUntil(this.destroy$)
    ).subscribe(res => {
      console.log(res);
      this.searchUserResults.find(_ => _.userId === to)!.isFollow = !isFollow;
    });
  }

  mergeAndShufflePosts()
  {
    this.allPosts = [...this.myPosts, ...this.followingPosts];
    this.allPosts = this.allPosts.sort((a, b) => 0.5 - Math.random());
  }

  isFollowPost(obj: any): obj is GetFollowPostsDTO
  {
    return "flName" in obj;
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
