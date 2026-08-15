import { Component, inject, signal } from '@angular/core';
import { NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { TopBar } from './components/top-bar/top-bar';
import { Menu } from './components/menu/menu';

@Component({
  selector: 'app-root',
  imports: [Menu, TopBar, RouterOutlet],
  template: `<app-top-bar />
  <div class="flex justify-between gap-0">
      <div class="flex w-auto me-2">
        <app-menu />
      </div>     
      <div class="flex flex-wrap w-full flex-col justify-center ms-1 mt-1 h-full scrollbar-gutter:stable">
        <router-outlet />
      </div>
    </div>`
})
export class App {
  protected readonly title = signal('auction-woodcuts-engravings');

  private router = inject(Router);

  ngOnInit(): void {
    this.router.events.subscribe((data) => {
      if(data instanceof NavigationEnd) {
        const navigation: NavigationEnd = data;
        if(navigation.url.startsWith('/create-or-update-auction') || navigation.url.startsWith('/auction-details')) {
         
        } else {

        }
      }
    });
  }
}
