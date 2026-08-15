import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/all-auctions-page/all-auctions-page').then((m) => m.AllAuctionsPage),
  },
  {
    path: 'auction-details/:id',
    loadComponent: () => import('./pages/auction-details-page/auction-details-page').then((m) => m.AuctionDetailsPage),
  },
  {
    path: 'create-or-update-auction/:id',
    loadComponent: () => import('./pages/create-or-update-auction/create-or-update-auction').then((m) => m.CreateOrUpdateAuction),
  },
  {
    path: 'auctions-overview',
    loadComponent: () => import('./pages/auction-overview-page/auction-overview-page').then((m) => m.AuctionOverviewPage),
  },
  {
    path: 'auctions-won',
    loadComponent: () => import('./pages/auctions-won-page/auctions-won-page').then((m) => m.AuctionsWonPage)
  }
];
