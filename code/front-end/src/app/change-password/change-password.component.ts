import {Component, OnInit} from '@angular/core';
import {CustomSnackbarService} from '../services/custom-snackbar.service';
import {ErrorHandlerService} from '../services/error-handler.service';
import {Router} from '@angular/router';
import {EndpointsService} from '../services/shared/endpoints.service';
import {OurCookieService} from '../services/shared/our-cookie.service';
import {FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material';
import {TsConstants} from '../constants/tsConstants';
import {ChangePassword} from '../models/change-password';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  changePassword: FormGroup;
  hide = true;
  hideConf = true;
  hideOld = true;
  matcher = new MyErrorStateMatcher();

  constructor(private formBuilder: FormBuilder,
              private generalService: EndpointsService,
              private router: Router,
              private errorHandler: ErrorHandlerService,
              private snackBar: CustomSnackbarService) {
  }

  ngOnInit() {
    this.changePassword = this.formBuilder.group({
      passwordF: ['', [Validators.required]],
      passwordC: [''],
      oldPassword: [''],
    }, {validator: this.checkPasswords});
  }

  onSubmit() {
    const changePassword: ChangePassword = {
      oldPassword: this.changePassword.value.oldPassword,
      newPassword: this.changePassword.value.passwordC
    };
    this.generalService.update<ChangePassword>(changePassword, TsConstants.APP_ENDPOINTS.USERS_CHANGE_PASSWORD).subscribe(
      success => {
        this.snackBar.successSnackBar('Your password has been updated!');
        setTimeout(() => {
          this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
        }, 2000);
      },
      error => {
        this.errorHandler.handleError(error);
      }
    );
  }

  private checkPasswords(group: FormGroup) {
    if (!group || !group.controls || !group.controls.passwordF || !group.controls.passwordF.value) {
      return;
    }
    const pass = group.controls.passwordF.value;
    const confirmPass = group.controls.passwordC.value;
    return pass === confirmPass ? null : {notSame: true};
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.touched);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.touched);
    return (invalidCtrl || invalidParent);
  }
}
