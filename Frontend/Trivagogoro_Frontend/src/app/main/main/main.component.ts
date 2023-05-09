import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit{
  activeList: any={
    'home': true,
    'map': false,
    'favorite': false,
    'user': false,
    'posting': false
  };

  destroy$: Subject<null>;

  constructor(
    private router: Router,
    private http: HttpClient
  )
  {
    this.destroy$ = new Subject<null>();
  }

  ngOnInit(): void {
    for(let key in this.activeList)
    {
      if(this.activeList[key])
      {
        this.router.navigate([`main/${key}`]);
      }
    }
  }

  activeIcon(type: string): void
  {
    for(let key in this.activeList)
    {
      if(key === type){
        this.activeList[key] = true;
      }else{
        this.activeList[key] = false;
      }
    }
  }
}
