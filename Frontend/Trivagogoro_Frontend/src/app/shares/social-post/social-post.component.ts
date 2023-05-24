import { Component, Input } from '@angular/core';
import { User } from 'src/app/models/Entities/User';
import { GetFollowPostsDTO } from 'src/app/models/Responses/GetFollowedPostDTO';
import { GetPostedPostRestDTO } from 'src/app/models/Responses/GetPostedPostRestRes';

@Component({
  selector: 'app-social-post',
  templateUrl: './social-post.component.html',
  styleUrls: ['./social-post.component.css']
})
export class SocialPostComponent {
  // @Input('user') user!: User;
  @Input('userName') userName!: string;
  @Input('post') post!: GetPostedPostRestDTO | GetFollowPostsDTO;

  constructor()
  {

  }

  getRandomAvatar()
  {
    if(Math.floor(Math.random()*3+1)%3 === 0)
    {
      return "../../../assets/images/default-avatar.png";
    }else if(Math.floor(Math.random()*3+1)%3 === 1)
    {
      return "../../../assets/images/avatar2.jpeg";
    }else{
      return "../../../assets/images/avatar3.png";
    }
  }
}
