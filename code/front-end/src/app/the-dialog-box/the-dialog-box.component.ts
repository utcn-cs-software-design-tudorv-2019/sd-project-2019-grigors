import {Component, Inject} from '@angular/core';
import {MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import {EndpointsService} from '../services/shared/endpoints.service';
import {Router} from '@angular/router';
import {TsConstants} from '../constants/tsConstants';
import {ErrorHandlerService} from '../services/error-handler.service';

@Component({
  selector: 'app-the-dialog-box',
  templateUrl: './the-dialog-box.component.html',
  styleUrls: ['./the-dialog-box.component.css']
})
export class TheDialogBoxComponent {
  imageSrc = '../../assets/default_element.png';
  constructor(public dialogRef: MatDialogRef<TheDialogBoxComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any,
              private router: Router,
              private generalService: EndpointsService,
              private errorHandler: ErrorHandlerService) {

    if ( this.data.elementImage != null) {
      this.imageSrc = this.data.elementImage;
    }
  }
  saveAttendance() {
    if (this.data.bookId != null) {
      const eventId: number = this.data.bookId;
      this.generalService.post<number>(eventId, `${TsConstants.APP_ENDPOINTS.ATTENDANCE}/${eventId}`).subscribe(resp => {
        this.dialogRef.close();
        localStorage.setItem('selectedTab', '1');
        this.router.navigate([TsConstants.ROUTES.MY_ELEMENTS]);
      }, error => {
        this.errorHandler.handleError(error);
      });
    }
  }
}
