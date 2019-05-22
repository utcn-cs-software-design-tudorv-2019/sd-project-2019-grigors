import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/index';
import {OurCookieService} from './our-cookie.service';
import {TsConstants} from '../../constants/tsConstants';

@Injectable({
  providedIn: 'root'
})
export class EndpointsService {
  public headers;
  constructor(private httpClient: HttpClient,
              private ourCookieService: OurCookieService) {
  }

  public getAll<T>(endpoint: string): Observable<T[]> {
    this.setRequestHeaders();
    return this.httpClient
      .get<T[]>(TsConstants.URL + endpoint, {headers: this.headers});
  }

  public getById<T>(input: number, endpoint: string): Observable<T> {
    this.setRequestHeaders();
    return this.httpClient
      .get<T>(TsConstants.URL + endpoint + '/' + input, {headers: this.headers});
  }

  public post<T>(input: any, endpoint: string): Observable<T> {
    this.setRequestHeaders();
    return this.httpClient
      .post<T>(TsConstants.URL + endpoint, input, {headers: this.headers});
  }

  public update<T>(input: any, endpoint: string): Observable<T> {
    this.setRequestHeaders();
    return this.httpClient
      .put<T>(TsConstants.URL + endpoint, input, {headers: this.headers});
  }

  public delete<T>(endpoint: string, id: number): Observable<T> {
    this.setRequestHeaders();
    return this.httpClient
      .delete<T>(`${TsConstants.URL }${endpoint}/${id}`, {headers: this.headers});
  }

  private setRequestHeaders() {
    const token = this.ourCookieService.getCookie(TsConstants.LOGGED_USER);
    this.headers = new HttpHeaders()
      .set('Content-Type', 'application/json')
      .set('MyAuthorization', token);
  }
}
