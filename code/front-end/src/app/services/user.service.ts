import {Injectable} from '@angular/core';
import {EndpointsService} from './shared/endpoints.service';
import {User} from '../models/user';
import {Observable} from 'rxjs/index';
import {HttpClient} from '@angular/common/http';
import {TsConstants} from '../constants/tsConstants';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private endPoints: EndpointsService, private httpClient: HttpClient) {
  }

  public getUserByToken<T>(endpoint: string): Observable<T[]> {
    return this.httpClient
      .get<T[]>(TsConstants.URL + endpoint, {headers: this.endPoints.headers});
  }

  async getUser(): Promise<User> {
    const item = await this.getUserByToken<User>(TsConstants.APP_ENDPOINTS.USERS_LOGGED_USER).toPromise();
    return new User(item);
  }
}
