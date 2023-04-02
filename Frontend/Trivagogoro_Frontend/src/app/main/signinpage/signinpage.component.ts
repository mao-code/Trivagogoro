import { SignInReq } from './../../models/Requests/SignInReq';
import { Router } from '@angular/router';
import { UserService } from './../../services/user/user.service';
import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { catchError, of, throwError, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-signinpage',
  templateUrl: './signinpage.component.html',
  styleUrls: ['./signinpage.component.css']
})
export class SigninpageComponent implements OnDestroy {
  signInForm = new FormGroup({
    account: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });

  get account(){ return this.signInForm.get("account") }
  get password(){ return this.signInForm.get("password") }

  destroy$: Subject<null>;

  constructor(
    private userService: UserService,
    private router: Router
  )
  {
    this.destroy$ = new Subject();
  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  signIn()
  {
    if(this.signInForm.invalid)
    {
      alert("Invalid SignIn format");
      return;
    }

    var info = this.signInForm.value;
    var signInReq: SignInReq = {
      account: info.account!,
      password: info.password!
    }

    this.userService.signIn(signInReq)
    .pipe(
      takeUntil(this.destroy$),
      catchError(err => {
        alert(err.error.message);
        return throwError(()=>err);
      })
    )
    .subscribe(res => {
      alert(res.message);
      if(res.isSuccess)
      {
        this.userService.isLoggedIn = true;
        this.router.navigate(['/main']);
      }
    });
  }

}
