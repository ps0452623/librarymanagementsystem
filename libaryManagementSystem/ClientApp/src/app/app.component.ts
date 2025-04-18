import { Component } from '@angular/core';
import { Router, Event, NavigationEnd } from '@angular/router';
import { environment } from 'environments/environment';
import { GoogleAnalyticsService } from 'ngx-google-analytics';

@Component({
  selector: 'app-root',
  templateUrl:'./app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(
    private router: Router,
    private $gaService: GoogleAnalyticsService // Changed to private if not used in the template
  ) {
    this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd && environment.production) {
        this.$gaService.pageView(event.url);
      }
    });
  }
}
