import { Injectable } from '@angular/core';
import {MatSnackBar} from '@angular/material';

@Injectable({
  providedIn: 'root'
})
export class CustomSnackbarService {

  constructor(private snackBar: MatSnackBar) { }

  successSnackBar(message: string): void {
    this.snackBar.open(message, 'X', {
      duration: 3000,
      panelClass: 'snackBarSuccess'
    });
  }
}
