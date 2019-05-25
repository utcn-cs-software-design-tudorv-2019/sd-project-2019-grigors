import {Book} from './book';
import {Movie} from './movie';

export class Comment {
  id?: number;
  title: string;
  body: string;
  date: Date;
  book?: Book;
  movie?: Movie;
  constructor(item: any) {
    if (typeof(item) === 'number') {
      this.id = item;
    } else {
      this.id = item.Id;
      this.title = item.Name;
      this.body = item.Description;
      this.date = item.Date;
      this.book = item.Book;
      this.movie = item.Movie;
    }
  }
}
