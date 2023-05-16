import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { User } from 'src/app/models/Entities/User';
import { GetPostedPostRestDTO } from 'src/app/models/Responses/GetPostedPostRestRes';
import { SocialService } from 'src/app/services/social/social.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-userpage',
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent implements OnDestroy, OnInit{
  user!: User;

  posts: GetPostedPostRestDTO[] = [];

  destroy$: Subject<null> = new Subject<null>();


  constructor(
    private userService: UserService,
    private socialService: SocialService
  )
  {
    this.userService.getUserInfo(this.userService.getUserId())
    .pipe(
      takeUntil(this.destroy$)
    ).subscribe(res => {
      this.user = res.data!;
    });

    this.socialService.getPostedPostRest(this.userService.getUserId())
    .pipe(
      takeUntil(this.destroy$)
    ).subscribe(res => {
      this.posts = res.data!.postedPostRest;
      console.log(res);
    })
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
