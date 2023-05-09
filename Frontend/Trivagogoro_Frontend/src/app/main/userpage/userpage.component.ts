import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { User } from 'src/app/models/Entities/User';
import { SocialService } from 'src/app/services/social/social.service';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-userpage',
  templateUrl: './userpage.component.html',
  styleUrls: ['./userpage.component.css']
})
export class UserpageComponent implements OnDestroy, OnInit{
  user!: User;

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
  }

  ngOnInit(): void {
    this.socialService.getPostedPostRest(this.userService.getUserId())
    .pipe(
      takeUntil(this.destroy$)
    ).subscribe(res => {
      console.log(res);
    })
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }
}
