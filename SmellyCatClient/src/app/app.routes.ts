import { Routes } from '@angular/router';
import { ContactComponent } from './contact/contact.component';

export const routes: Routes = [
  {
    path: 'contact',
    component: ContactComponent,
    title: 'Contact',
  },
  {
    path: '**',
    redirectTo: 'contact',
  },
];
