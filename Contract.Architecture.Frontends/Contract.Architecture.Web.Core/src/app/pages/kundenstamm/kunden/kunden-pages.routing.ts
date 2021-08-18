import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { KundeDetailPage } from './sub-pages/kunde-detail/kunde-detail.page';
import { KundenPage } from './kunden.page';

const routes: Routes = [
  { path: '', component: KundenPage },
  { path: 'detail/:id', component: KundeDetailPage },
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class KundenPagesRouting { }
