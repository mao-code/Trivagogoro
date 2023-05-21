import { SocialService } from './../../services/social/social.service';
import { Component, OnDestroy } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { User } from 'src/app/models/Entities/User';
import { GetPostedPostRestDTO } from 'src/app/models/Responses/GetPostedPostRestRes';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnDestroy{
  allPosts: GetPostedPostRestDTO[] = [];

  myPosts: GetPostedPostRestDTO[] = [];
  followingPosts: GetPostedPostRestDTO[] = [];

  destroy$: Subject<null>;

  tmpUser?: User;

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
      takeUntil(this.destroy$)
    )
    .subscribe(res => {
      console.log(res);
      if(res.data?.postedPostRest)
      {
        this.myPosts = res.data.postedPostRest;

        this.mergeAndShufflePosts();
        console.log(this.allPosts);
      }
    });


  }

  searchUser(event: KeyboardEvent)
  {
    if(event.key === "Enter")
    {
      // do search user
    }
  }

  mergeAndShufflePosts()
  {
    this.allPosts = [...this.myPosts, ...this.followingPosts];
    this.allPosts = this.allPosts.sort((a, b) => 0.5 - Math.random());
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
