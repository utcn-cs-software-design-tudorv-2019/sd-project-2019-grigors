import {Injectable} from '@angular/core';
import {EndpointsService} from './shared/endpoints.service';
import {BehaviorSubject} from 'rxjs';
import {Movie} from '../models/movie';
import {FilterElements} from '../models/filter-elements';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  movieIdSource = new BehaviorSubject<number>(1);
  currentMovieId = this.movieIdSource.asObservable();

  constructor(private endPoints: EndpointsService) { }

  async get(endpoint: string): Promise<Movie[]> {
    const movies: Movie[] = [];
    const moviesList = await this.endPoints.getAll<any[]>(endpoint).toPromise();
    for (const obj of moviesList) {
      const movie: Movie = new Movie(obj);
      movies.push(movie);
    }
    return movies;
  }

  async getMovieById(id: number, endpoint: string): Promise<Movie> {
    let movie: Movie;
    movie = new Movie(await this.endPoints.getById<any>(id, endpoint).toPromise());
    return movie;
  }

  changeMovieId(eventId: number) {
    this.movieIdSource.next(eventId);
  }

  async filter(filterElements: FilterElements, endpoint: string): Promise<Movie[]> {
    const movies: Movie[] = [];
    let movie: Movie;
    const moviesList = await this.endPoints.post<any[]>(filterElements, endpoint).toPromise();
    for (const obj of moviesList) {
      movie = new Movie(obj);
      movies.push(movie);
    }
    return movies;
  }
}
