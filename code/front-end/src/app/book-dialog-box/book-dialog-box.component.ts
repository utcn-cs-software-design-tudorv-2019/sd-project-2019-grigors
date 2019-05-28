import {Component, Input} from '@angular/core';
import {MatDialog} from '@angular/material';
import {TheDialogBoxComponent} from '../the-dialog-box/the-dialog-box.component';
import {Book} from '../models/book';
import {TsConstants} from '../constants/tsConstants';
import {Movie} from '../models/movie';
import {EndpointsService} from '../services/shared/endpoints.service';
import {MoviesService} from '../services/movies.service';


@Component({
  selector: 'app-element-dialog-box',
  templateUrl: './book-dialog-box.component.html',
  styleUrls: ['./book-dialog-box.component.css']
})

export class BookDialogBoxComponent {
  @Input() dialogBook: Book;
  movie: Movie;

  constructor(public dialog: MatDialog, private endpointsService: MoviesService) {
  }


  async openDialog() {
    let author_director;
    let edition_year;
    let comments;
    author_director = this.dialogBook.author;
    edition_year = this.dialogBook.edition;
    if (this.dialogBook.name.includes(TsConstants.BOOK_SUFFIX)) {
      comments = this.dialogBook.comments;
    } else {
      this.movie = await this.endpointsService.getMovieById(this.dialogBook.id, TsConstants.APP_ENDPOINTS.MOVIES);
      comments = this.movie.comments;
    }
    const dialogRef = this.dialog.open(TheDialogBoxComponent,
      {
        width: '460px',
        height: '580px',
        data: {
          elementName: this.dialogBook.name,
          elementAuthor_Director: author_director,
          elementDescription: this.dialogBook.description,
          elementEdition_Year: edition_year,
          elementId: this.dialogBook.id,
          elementImage: this.dialogBook.image,
          elementComments: comments,
        }
      }
    );
  }
}

