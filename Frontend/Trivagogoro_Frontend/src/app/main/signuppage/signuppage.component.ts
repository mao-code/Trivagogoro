import { UserService } from './../../services/user/user.service';
import { RegisterReq } from './../../models/Requests/RegisterReq';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, Subject, takeUntil, throwError } from 'rxjs';

@Component({
  selector: 'app-signuppage',
  templateUrl: './signuppage.component.html',
  styleUrls: ['./signuppage.component.css']
})
export class SignuppageComponent implements OnInit, OnDestroy{
  registerForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', Validators.required)
  });

  get userName(){ return this.registerForm.get("userName") }
  get email(){ return this.registerForm.get("email") }
  get password(){ return this.registerForm.get("password") }

  destroy$: Subject<any>;

  constructor(
    private registerService: UserService,
    private router: Router
  )
  {
    this.destroy$ = new Subject<null>();
  }

  ngOnInit(): void {

  }

  ngOnDestroy(): void {
    this.destroy$.next(null);
    this.destroy$.complete();
  }

  register(): void
  {
    if(this.registerForm.invalid)
    {
      alert("Invalid Register format");
      return;
    }

    var info = this.registerForm.value;
    var registerDto: RegisterReq = {
      name: info.userName!,
      account: info.email!,
      password: info.password!
    }

    this.registerService.register(registerDto)
    .pipe(
      takeUntil(this.destroy$),
      catchError(err => {
        alert(err.error.message);
        return throwError(()=>err);
      })
    )
    .subscribe(res => {
      if(res.isSuccess)
      {
        console.log(res);
        alert("Register Successfully");
        this.router.navigate(['/init']);
      }else{
        alert(res.message);
      }
    });
  }
}

