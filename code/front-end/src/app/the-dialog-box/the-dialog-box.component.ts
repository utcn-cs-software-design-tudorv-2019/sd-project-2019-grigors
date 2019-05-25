import {Component, Inject, OnInit} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import {EndpointsService} from '../services/shared/endpoints.service';
import {Router} from '@angular/router';
import {TsConstants} from '../constants/tsConstants';
import {ErrorHandlerService} from '../services/error-handler.service';
import {FormBuilder, FormGroup} from '@angular/forms';
import {Comment} from '../models/comment';
import {CustomSnackbarService} from '../services/custom-snackbar.service';
import {ViewEncapsulation} from '@angular/cli/lib/config/schema';
import {Book} from '../models/book';
import {BehaviorSubject} from 'rxjs';
import {BooksService} from '../services/books.service';
import {Movie} from '../models/movie';
import {MoviesService} from '../services/movies.service';

@Component({
  selector: 'app-the-dialog-box',
  templateUrl: './the-dialog-box.component.html',
  styleUrls: ['./the-dialog-box.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class TheDialogBoxComponent implements OnInit {

  createCommentForm: FormGroup;
  comment: Comment;
  newCommentFinal: Comment;

  book: Book;
  movie: Movie;

  imageSrc = '';

  constructor(public dialogRef: MatDialogRef<TheDialogBoxComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private router: Router,
              private errorHandler: ErrorHandlerService,
              private formBuilder: FormBuilder,
              private booksService: BooksService,
              private moviesService: MoviesService,
              private endPoints: EndpointsService,
              private snackBar: CustomSnackbarService,
  ) {
    if (this.data.elementImage != null) {
      this.imageSrc = this.data.elementImage;
    }
    this.createCommentForm = this.formBuilder.group({
      title: [''],
      body: [''],
      date: new Date(),
    });
  }

  private createComment(): Comment {
    const createCommentValue = this.createCommentForm.value;
    const newComment: Comment = {
      title: createCommentValue.title,
      body: createCommentValue.body,
      date: createCommentValue.date,
    };
    return newComment;
  }

  async ngOnInit() {
    if (this.data.elementName.includes(TsConstants.BOOK_SUFFIX)) {
      this.book = await this.booksService.getBookById(this.data.elementId, TsConstants.APP_ENDPOINTS.BOOKS);
    } else {
      if (this.data.elementName.includes(TsConstants.MOVIE_SUFFIX)) {
        this.movie = await this.moviesService.getMovieById(this.data.elementId, TsConstants.APP_ENDPOINTS.MOVIES);
      }
    }
  }

  onSave() {
    this.newCommentFinal = this.createComment();
    if (this.data.elementName.includes(TsConstants.BOOK_SUFFIX)) {
      this.newCommentFinal.book = this.book;
      this.endPoints.post<Book>(this.newCommentFinal, TsConstants.APP_ENDPOINTS.COMMENTS).subscribe(
        succes => {
          this.snackBar.successSnackBar('Your comment has been added!');
          setTimeout(() => {
            this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
          }, 2000);
        },
        error => {
          this.errorHandler.handleError(error);
        }
      );
    }
    if (this.data.elementName.includes(TsConstants.MOVIE_SUFFIX)) {
      this.newCommentFinal.movie = this.movie;
      this.endPoints.post<Movie>(this.newCommentFinal, TsConstants.APP_ENDPOINTS.COMMENTS).subscribe(
        succes => {
          this.snackBar.successSnackBar('Your comment has been added!');
          setTimeout(() => {
            this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
          }, 2000);
        },
        error => {
          this.errorHandler.handleError(error);
        }
      );
    }
  }

}
