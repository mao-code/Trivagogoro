import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Component } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  activeList: any={
    'map': true,
    'favorite': false,
    'explore': false,
    'user': false
  };

  destroy$: Subject<null>;

  constructor(
    private router: Router
  )
  {
    this.destroy$ = new Subject<null>();

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
