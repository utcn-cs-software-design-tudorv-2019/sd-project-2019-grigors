import {NgModule} from '@angular/core';
import {ReactiveFormsModule, FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {AppComponent} from './app.component';
import {ElementComponent} from './element/element.component';
import {LogInFormComponent} from './log-in-form/log-in-form.component';
import {LoginService} from './services/login.service';
import {
  MatTabsModule, MatButtonModule, MatInputModule, MatCardModule, MatIconModule, MatToolbarModule,
  MatNativeDateModule, MatRadioModule, MatSnackBarModule, MatMenuModule, MatProgressBarModule,
  MatProgressSpinnerModule, MatTableModule
} from '@angular/material';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatListModule} from '@angular/material/list';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatSelectModule} from '@angular/material/select';
import {CreateBookComponent} from './create-book/create-book.component';
import {CreateMovieComponent} from './create-movie/create-movie.component';
import {CookieService} from 'ngx-cookie-service';
import {CreateUserComponent} from './create-user/create-user.component';
import {AppRoutingModule} from './app-routing.module';
import {DashboardComponent} from './dashboard/dashboard.component';
import {HeaderComponent} from './header/header.component';
import {FlexLayoutModule} from '@angular/flex-layout';
import {BookDialogBoxComponent} from './book-dialog-box/book-dialog-box.component';
import {TheDialogBoxComponent} from './the-dialog-box/the-dialog-box.component';
import {BooksService} from './services/books.service';
import {MoviesService} from './services/movies.service';
import {AuthGuard} from './services/auth-guard.service';
import {EndpointsService} from './services/shared/endpoints.service';
import {TsConstants} from './constants/tsConstants';
import {MyElementsComponent} from './my-elements/my-elements.component';
import {OurCookieService} from './services/shared/our-cookie.service';
import {MatDialogModule} from '@angular/material/dialog';
import {MatDividerModule} from '@angular/material/divider';
import {ErrorHandlerService} from './services/error-handler.service';
import {CustomSnackbarService} from './services/custom-snackbar.service';
import {ChangePasswordComponent} from './change-password/change-password.component';
import {ConfirmDialogComponent} from './confirm-dialog/confirm-dialog.component';

const ANGULAR_MATERIAL = [
  MatInputModule,
  MatButtonModule,
  MatCardModule,
  MatRadioModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatToolbarModule,
  MatIconModule,
  MatCardModule,
  MatTabsModule,
  MatListModule,
  MatDialogModule,
  MatDividerModule,
  MatSelectModule,
  MatSnackBarModule,
  MatMenuModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatTableModule
];
@NgModule({
  declarations: [
    AppComponent,
    ElementComponent,
    CreateBookComponent,
    CreateMovieComponent,
    CreateUserComponent,
    LogInFormComponent,
    DashboardComponent,
    HeaderComponent,
    BookDialogBoxComponent,
    TheDialogBoxComponent,
    MyElementsComponent,
    ChangePasswordComponent,
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatSelectModule,
    ReactiveFormsModule,
    MatIconModule,
    ReactiveFormsModule,
    FlexLayoutModule,
    AppRoutingModule,
    ANGULAR_MATERIAL
  ],
  providers: [
    CookieService,
    OurCookieService,
    BooksService,
    MoviesService,
    AuthGuard,
    LoginService,
    EndpointsService,
    TsConstants,
    MatDatepickerModule,
    ErrorHandlerService,
    ...ANGULAR_MATERIAL,
    FlexLayoutModule,
    HttpClientModule,
    CustomSnackbarService,
  ],
  entryComponents: [
    BookDialogBoxComponent,
    TheDialogBoxComponent,
    ConfirmDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
