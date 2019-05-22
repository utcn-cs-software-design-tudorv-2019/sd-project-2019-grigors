import {Injectable} from '@angular/core';
import {CanActivate, Router} from '@angular/router';
import {OurCookieService} from './shared/our-cookie.service';
import {TsConstants} from '../constants/tsConstants';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private loginCookieService: OurCookieService, private router: Router) {
  }

  canActivate(): boolean {
    const cookie = this.loginCookieService.getCookie(TsConstants.LOGGED_USER);
    if (!cookie) {
      this.router.navigate([TsConstants.ROUTES.LOGIN]);
      return false;
    }
    if (cookie.slice(cookie.length - 8, cookie.length) !== TsConstants.ADMIN_TOKEN_END) {
      return true;
    }
    this.router.navigate([TsConstants.ROUTES.ADMIN]);
    return false;
  }

}
