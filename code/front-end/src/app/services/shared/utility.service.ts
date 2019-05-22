import {Injectable} from '@angular/core';
import {TsConstants} from '../../constants/tsConstants';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs/index';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  constructor(private httpClient: HttpClient) { }
  private headers = new HttpHeaders()
    .set('Content-Type', 'application/json');
  public getAll<T>(endpoint: string): Observable<T[]> {
    return this.httpClient
      .get<T[]>(TsConstants.URL + endpoint, {headers: this.headers});
  }
  public post<T>(input: any, endpoint: string): Observable<T> {
    return this.httpClient
      .post<T>(TsConstants.URL  + endpoint, input, {headers: this.headers});
  }

}
