import { Component, Input } from '@angular/core';
import { User } from 'src/app/models/Entities/User';
import { GetPostedPostRestDTO } from 'src/app/models/Responses/GetPostedPostRestRes';

@Component({
  selector: 'app-social-post',
  templateUrl: './social-post.component.html',
  styleUrls: ['./social-post.component.css']
})
export class SocialPostComponent {
  @Input('user') user!: User;
  @Input('post') post!: GetPostedPostRestDTO;

  constructor()
  {

  }
}
