import {Component, Input, OnInit} from '@angular/core';
import {Book} from '../models/book';
import {TsConstants} from '../constants/tsConstants';
import {BooksService} from '../services/books.service';
import {Movie} from '../models/movie';
import {MoviesService} from '../services/movies.service';

@Component({
  selector: 'app-element',
  templateUrl: './element.component.html',
  styleUrls: ['./element.component.css']
})
export class ElementComponent implements OnInit {

  @Input() book: Book;
  @Input() movie: Movie;
  @Input() isOwned: boolean;
  @Input() isOwnedMovie: boolean;
  bookId: number;
  path_image: any;

  constructor(private booksService: BooksService, private moviesService: MoviesService) {
  }

  ngOnInit() {
    this.booksService.currentBookId.subscribe(bookId => this.bookId = bookId);
    this.path_image = this.book.image;
  }

  newBookId() {
    this.booksService.changeBookId(this.book.id);
  }

  newMovieId() {
    this.moviesService.changeMovieId(this.book.id);
  }
}
