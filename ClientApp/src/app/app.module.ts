import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { HostingComponent } from './hosting/hosting.component';
import { ListingformComponent } from './listing/listingform.component';
import { UserhistoryComponent } from './userhistory/userhistory.component';
import { ListingdetailsComponent } from './listingdetails/listingdetails.component';
import { ListingformUpdateComponent } from './listing/listingformupdate.component';
//import { searchComponent } from './search/search.component';






@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    HostingComponent,
    ListingformComponent,
    UserhistoryComponent,
    ListingdetailsComponent,
    ListingformUpdateComponent,
    
  ],

  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'hosting', component: HostingComponent, canActivate: [AuthorizeGuard] },
      { path: 'createListing', component: ListingformComponent, canActivate: [AuthorizeGuard] },
      { path: 'listingFormUpdate/:id', component: ListingformUpdateComponent, canActivate: [AuthorizeGuard] }, 
      { path: 'userhistory', component: UserhistoryComponent, canActivate: [AuthorizeGuard] },
      { path: 'listingdetails/:id', component: ListingdetailsComponent },
      //{ path: 'search', component: searchComponent },

     
    ])
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    //SharedListingsService,

  ],


  bootstrap: [AppComponent]
})
export class AppModule { }
