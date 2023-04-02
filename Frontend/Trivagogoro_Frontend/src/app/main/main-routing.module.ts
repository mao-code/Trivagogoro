import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main/main.component';

const routes: Routes = [
  {
    path: '',
    component: MainComponent,
    children: [
      {
        path: '',
        redirectTo: 'map',
        pathMatch: 'full'
      },
      {
        path: 'user',
        loadChildren: () => import('./userpage/userpage.module').then(m => m.UserpageModule),
      },
      {
        path: 'map',
        loadChildren: () => import('./mappage/mappage.module').then(m => m.MappageModule)
      },
      {
        path: 'favorite',
        loadChildren: () => import('./favoritepage/favoritepage.module').then(m => m.FavoritepageModule)
      },
      {
        path: 'explore',
        loadChildren: () => import('./explorepage/explorepage.module').then(m => m.ExplorepageModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MainRoutingModule { }
