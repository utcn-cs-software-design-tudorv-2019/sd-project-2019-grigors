import {Component, OnInit} from '@angular/core';
import {Book} from '../models/book';
import {BooksService} from '../services/books.service';
import {TsConstants} from '../constants/tsConstants';
import {FilterElements} from '../models/filter-elements';
import {FormBuilder, FormGroup} from '@angular/forms';
import {ViewEncapsulation} from '@angular/cli/lib/config/schema';
import {MoviesService} from '../services/movies.service';
import {Movie} from '../models/movie';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class DashboardComponent implements OnInit {
  books: Book[] = null;
  movies: Movie[] = null;
  filterForm: FormGroup;
  selectedTab = 0;
  constructor(private booksService: BooksService,
              private moviesService: MoviesService,
              private formBuilder: FormBuilder) {
    this.filterForm = this.formBuilder.group({
      name: [''],
    });
  }

  async ngOnInit() {
    this.books = await this.booksService.get(TsConstants.APP_ENDPOINTS.BOOKS_DASHBOARD);
    this.movies = await this.moviesService.get(TsConstants.APP_ENDPOINTS.MOVIES_DASHBOARD);
    if (localStorage.getItem('selectedTab') !== null) {
      this.selectedTab = +localStorage.getItem('selectedTab');
      localStorage.removeItem('selectedTab');
    }
  }

  async filterBooks() {
    const filterElements: FilterElements = new FilterElements();
    const filterFormValues = this.filterForm.value;
    if (filterFormValues.name !== '') {
      filterElements.name = filterFormValues.name;
    }
    this.books = await this.booksService.filter(filterElements, TsConstants.APP_ENDPOINTS.BOOKS_FILTER);
  }
  async filterMovies() {
    const filterElements: FilterElements = new FilterElements();
    const filterFormValues = this.filterForm.value;
    if (filterFormValues.name !== '') {
      filterElements.name = filterFormValues.name;
    }
    this.movies = await this.moviesService.filter(filterElements, TsConstants.APP_ENDPOINTS.MOVIES_FILTER);
  }
  resetForm() {
    this.filterForm.value.title = '';
    this.filterForm.get('name').setValue('');
    this.filterBooks();
    this.filterMovies();
  }

}
