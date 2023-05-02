import { SignInReq } from './../../models/Requests/SignInReq';
import { environment } from './../../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ResponseData } from 'src/app/models/ResponseData';
import { RegisterReq } from 'src/app/models/Requests/RegisterReq';
import { Observable } from 'rxjs';
import { SigninRes } from 'src/app/models/Responses/SigninRes';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseURI: string;

  // simple way: when signin, set to true. Otherwise, false;
  isLoggedIn: boolean;

  constructor(private http: HttpClient) {
    this.baseURI = `${environment.domain}/${environment.baseRoute.user}`;
    this.isLoggedIn = false;
  }

  register(req: RegisterReq): Observable<ResponseData<null>>
  {
    return this.http.post<ResponseData<null>>(`${this.baseURI}/signup`, req);
  }

  signIn(req: SignInReq): Observable<ResponseData<SigninRes>>
  {
    return this.http.post<ResponseData<SigninRes>>(`${this.baseURI}/signin`, req);
  }

  setUserId(userId: number)
  {
    localStorage.setItem("userId", userId.toString());
  }

  getUserId(): number
  {
    return Number.parseInt(localStorage.getItem("userId")!);
  }
}
