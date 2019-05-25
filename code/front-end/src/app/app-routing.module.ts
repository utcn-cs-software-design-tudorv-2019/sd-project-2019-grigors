import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';
import {LogInFormComponent} from './log-in-form/log-in-form.component';
import {AuthGuard} from './services/auth-guard.service';
import {MyElementsComponent} from './my-elements/my-elements.component';
import {CreateUserComponent} from './create-user/create-user.component';
import {CreateBookComponent} from './create-book/create-book.component';
import {ChangePasswordComponent} from './change-password/change-password.component';
import {CreateMovieComponent} from './create-movie/create-movie.component';

const routes: Routes = [
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LogInFormComponent },
  { path: 'register', component: CreateUserComponent },
  { path: 'create-book', component: CreateBookComponent, pathMatch: 'full' },
  { path: 'create-movie', component: CreateMovieComponent, pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'my-elements', component: MyElementsComponent, canActivate: [AuthGuard] },
  { path: 'account', component: CreateUserComponent, canActivate: [AuthGuard] },
  { path: 'books/update', component: CreateBookComponent, canActivate: [AuthGuard] },
  { path: 'change-password', component: ChangePasswordComponent, canActivate: [AuthGuard] },
];


@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule {}
