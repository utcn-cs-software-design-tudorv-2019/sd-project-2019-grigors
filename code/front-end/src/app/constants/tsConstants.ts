import {Injectable} from '@angular/core';

@Injectable()
export class TsConstants {
  static LOGGED_USER = 'loggedUserToken';
  static ADMIN_TOKEN_END  = '61387333';
  static URL = 'api/';

  static APP_ENDPOINTS = {
    CATEGORIES: 'categories',
    USERS: 'users',
    LOGIN: 'login',
    USERS_LOGGED_USER: 'users/loggedUser',
    USERS_CHANGE_PASSWORD: 'users/changePassword',
    BOOKS: 'books',
    MOVIES: 'movies',
    EVENTS_ATTEND: 'books/attend',
    BOOKS_MY_BOOKS: 'books/myBooks',
    MOVIES_MY_MOVIES: 'movies/myMovies',
    BOOKS_DASHBOARD: 'books/dashboard',
    MOVIES_DASHBOARD: 'movies/dashboard',
    BOOKS_FILTER: 'books/filter',
    MOVIES_FILTER: 'movies/filter',
    ATTENDANCE: 'attendance',
  };

  static ROUTES = {
    DASHBOARD: 'dashboard',
    LOGIN: 'login',
    MY_ELEMENTS: 'my-elements',
    REGISTER: 'register',
    ADMIN: 'admin',
    BOOKS_UPDATE: '/books/update',
    MOVIES_UPDATE: '/movies/update'
  };
}
