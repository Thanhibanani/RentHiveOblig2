import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from '../../api-authorization/authorize.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {

  constructor(private authorizeService: AuthorizeService) { }

  isLoggedIn: boolean = false;


  ngOnInit() {
    this.authorizeService.isAuthenticated().subscribe(
      (authStatus) => {
        this.isLoggedIn = authStatus;
      },
      (error) => {
        console.error('Error checking authentication status', error);
      }
    );
  }


  isExpanded = false;
  //Denne funksjonaliteten kan fjernes siden vi ikke trenger å skru nav baren av og på
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
