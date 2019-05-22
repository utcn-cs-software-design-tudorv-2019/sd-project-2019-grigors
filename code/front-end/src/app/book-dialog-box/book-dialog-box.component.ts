import {Component, Input} from '@angular/core';
import {MatDialog} from '@angular/material';
import {TheDialogBoxComponent} from '../the-dialog-box/the-dialog-box.component';
import {Book} from '../models/book';
import {BooksService} from '../services/books.service';
import {TsConstants} from '../constants/tsConstants';
import {Movie} from '../models/movie';

@Component({
  selector: 'app-element-dialog-box',
  templateUrl: './book-dialog-box.component.html',
  styleUrls: ['./book-dialog-box.component.css']
})

export class BookDialogBoxComponent {
  @Input() dialogBook: Book;
  @Input() dialogMovie: Movie;

  constructor(public dialog: MatDialog) {
  }

  async openDialog() {
    let author_director;
    let edition_year;

    author_director = this.dialogBook.author;
    edition_year = this.dialogBook.edition;

    // if (this.dialogBook.author == null) {
    //   author_director = this.dialogMovie.author;
    // } else {
    //   author_director = this.dialogBook.author;
    // }
    //
    // if (this.dialogBook.edition == null) {
    //   edition_year = this.dialogMovie.edition;
    // } else {
    //   edition_year = this.dialogBook.edition;
    // }

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
        }
      }
    );
  }
}

