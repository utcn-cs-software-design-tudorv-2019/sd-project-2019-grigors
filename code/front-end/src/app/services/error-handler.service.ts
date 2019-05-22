import { ErrorHandler, Injectable} from '@angular/core';
import {UNAUTHORIZED, BAD_REQUEST,  NOT_FOUND} from 'http-status-codes';
import {Router} from '@angular/router';
import {MatSnackBar} from '@angular/material';

@Injectable()
export class ErrorHandlerService implements ErrorHandler {

  constructor( private snackBar: MatSnackBar) { }

  public handleError(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error, ${error.error}`;
    } else {
      errorMessage = error.error;
    }
    this.snackBar.open(errorMessage, 'X', {
      duration: 5000,
      panelClass: 'snackBarError'
    });
  }
}
