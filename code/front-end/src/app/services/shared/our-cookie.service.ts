import {Injectable} from '@angular/core';
import {CookieService} from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class OurCookieService {

  constructor(private cookieService: CookieService) { }

  setCookie(input: any, key: string) {
    if (!input || !key) {return; }
    this.cookieService.set(key, input);
  }

  getCookie(key: string) {
    return this.cookieService.get(key);
  }

  deleteCookie(key: string) {
    this.cookieService.delete(key);
  }
}
