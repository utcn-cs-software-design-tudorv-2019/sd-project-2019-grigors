import {Component, Inject} from '@angular/core';
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

@Component({
  selector: 'app-the-dialog-box',
  templateUrl: './the-dialog-box.component.html',
  styleUrls: ['./the-dialog-box.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class TheDialogBoxComponent {

  createCommentForm: FormGroup;
  comment: Comment;
  newCommentFinal: Comment;

  book: Book;
  newBook: Book;

  imageSrc = '';

  elementIdSource = new BehaviorSubject<number>(1);
  currentElementId = this.elementIdSource.asObservable();
  elementId: number;

  constructor(public dialogRef: MatDialogRef<TheDialogBoxComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private router: Router,
              private generalService: EndpointsService,
              private errorHandler: ErrorHandlerService,
              private formBuilder: FormBuilder,
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
    this.currentElementId.subscribe(elementId => this.elementId = elementId);
    this.book = await(new Book( this.endPoints.getById<Book> (this.elementId, TsConstants.APP_ENDPOINTS.BOOKS).toPromise()));
  }

  onSave() {
    this.newCommentFinal = this.createComment();
    this.newBook.id = this.book.id;
    this.newBook.comments.push(this.newCommentFinal);
    this.endPoints.update<Book>(this.newBook, TsConstants.APP_ENDPOINTS.BOOKS).subscribe(
      succes => {
        this.snackBar.successSnackBar('Your comment has been added!');
        setTimeout(() => {
          this.router.navigate(['dashboard']);
        }, 2000);
      },
      error => {
        this.errorHandler.handleError(error);
      }
    );
  }

}
