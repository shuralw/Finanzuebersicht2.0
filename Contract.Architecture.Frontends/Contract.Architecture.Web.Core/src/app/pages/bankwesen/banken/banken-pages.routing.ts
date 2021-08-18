import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BankDetailPage } from './sub-pages/bank-detail/bank-detail.page';
import { BankenPage } from './banken.page';

const routes: Routes = [
  { path: '', component: BankenPage },
  { path: 'detail/:id', component: BankDetailPage },
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class BankenPagesRouting { }
