import {Component, OnInit} from '@angular/core';
import {BooksService} from '../services/books.service';
import {Movie} from '../models/movie';
import {Book} from '../models/book';
import {TsConstants} from '../constants/tsConstants';
import {MoviesService} from '../services/movies.service';


@Component({
  selector: 'app-my-events',
  templateUrl: './my-elements.component.html',
  styleUrls: ['./my-elements.component.css']
})
export class MyElementsComponent implements OnInit {

  myBooks: Book[];
  myMovies: Movie[];
  selectedTab = 0;

  constructor(private booksService: BooksService,
              private moviesService: MoviesService) {
  }

  async ngOnInit() {
    this.myBooks = await this.booksService.get(TsConstants.APP_ENDPOINTS.BOOKS_MY_BOOKS);
    this.myMovies = await this.moviesService.get(TsConstants.APP_ENDPOINTS.MOVIES_MY_MOVIES);
    if (localStorage.getItem('selectedTab') !== null) {
      this.selectedTab = +localStorage.getItem('selectedTab');
      localStorage.removeItem('selectedTab');
    }
  }
}
