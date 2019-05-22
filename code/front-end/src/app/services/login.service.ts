import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {LoginUser} from '../models/login-user';
import {UtilityService} from './shared/utility.service';
import {TsConstants} from '../constants/tsConstants';

@Injectable({
  providedIn: 'root'
})

export class LoginService {
  constructor(private utilityService: UtilityService) {
  }
  checkLogin(email: string, password: string): Observable<any> {
    if (!email || !password) { return; }
    const userToLogin = new LoginUser();
    userToLogin.email = email;
    userToLogin.password = password;
    const jsonUserToLogIn = JSON.stringify(userToLogin);
    return this.utilityService.post<LoginUser>(jsonUserToLogIn,  TsConstants.APP_ENDPOINTS.LOGIN);
  }

}
