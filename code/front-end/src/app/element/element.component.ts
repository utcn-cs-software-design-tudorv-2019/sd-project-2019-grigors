import {Component, Input, OnInit} from '@angular/core';
import {Book} from '../models/book';
import {TsConstants} from '../constants/tsConstants';
import {BooksService} from '../services/books.service';
import {Movie} from '../models/movie';

@Component({
  selector: 'app-element',
  templateUrl: './element.component.html',
  styleUrls: ['./element.component.css']
})
export class ElementComponent implements OnInit {

  @Input() book: Book;
  @Input() movie: Movie;
  @Input() isOwned: boolean;
  bookId: number;

  constructor(private booksService: BooksService) { }

  ngOnInit() {
    this.booksService.currentBookId.subscribe(bookId => this.bookId = bookId);
  }

  newBookId() {
    this.booksService.changeBookId(this.book.id);
  }

 }
