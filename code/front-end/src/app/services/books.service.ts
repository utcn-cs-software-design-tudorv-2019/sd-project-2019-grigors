import {Injectable} from '@angular/core';
import {Book} from '../models/book';
import {EndpointsService} from './shared/endpoints.service';
import {BehaviorSubject} from 'rxjs';
import {FilterElements} from '../models/filter-elements';

@Injectable({
  providedIn: 'root'
})
export class BooksService {

  bookIdSource = new BehaviorSubject<number>(1);
  currentBookId = this.bookIdSource.asObservable();

  constructor(private endPoints: EndpointsService) { }

  async get(endpoint: string): Promise<Book[]> {
    const books: Book[] = [];
    const events = await this.endPoints.getAll<any[]>(endpoint).toPromise();
    for (const obj of events) {
      const event: Book = new Book(obj);
      books.push(event);
    }
    return books;
  }

  async getBookById(id: number, endpoint: string): Promise<Book> {
    let yonderEvent: Book;
    yonderEvent = new Book(await this.endPoints.getById<any>(id, endpoint).toPromise());
    return yonderEvent;
  }

  changeBookId(eventId: number) {
    this.bookIdSource.next(eventId);
  }

  async filter(filterElements: FilterElements, endpoint: string): Promise<Book[]> {
    const books: Book[] = [];
    let book: Book;
    const booksList = await this.endPoints.post<any[]>(filterElements, endpoint).toPromise();
    for (const obj of booksList) {
      book = new Book(obj);
      books.push(book);
    }
    return books;
  }
}
