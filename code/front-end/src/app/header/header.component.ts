import {Component, DoCheck, OnInit} from '@angular/core';
import {OurCookieService} from '../services/shared/our-cookie.service';
import {TsConstants} from '../constants/tsConstants';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements DoCheck {

  public homeurl: string;
  public isLogged: boolean;
  public adminHeader: boolean;

  ngDoCheck() {
    if (!this.loginCookieService.getCookie(TsConstants.LOGGED_USER)) {
      return;
    } else {
      this.isLogged = true;
      const cookie = this.loginCookieService.getCookie(TsConstants.LOGGED_USER);
      this.adminHeader = cookie.slice(cookie.length - 8, cookie.length) === TsConstants.ADMIN_TOKEN_END;
      this.homeurl = '/dashboard';
    }
  }

  constructor(private loginCookieService: OurCookieService) {
  }

  pressedLogOut() {
    this.loginCookieService.deleteCookie(TsConstants.LOGGED_USER);
    this.isLogged = false;
    this.adminHeader = false;
  }

}
