import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, FormGroupDirective, NgForm, Validators} from '@angular/forms';
import {User} from '../models/user';
import {EndpointsService} from '../services/shared/endpoints.service';
import {OurCookieService} from '../services/shared/our-cookie.service';
import {Router} from '@angular/router';
import {TsConstants} from '../constants/tsConstants';
import {ErrorStateMatcher} from '@angular/material';
import {UserService} from '../services/user.service';
import {ErrorHandlerService} from '../services/error-handler.service';
import {CustomSnackbarService} from '../services/custom-snackbar.service';
import {UtilityService} from '../services/shared/utility.service';
import {ViewEncapsulation} from '@angular/cli/lib/config/schema';

@Component({
  selector: 'app-create-user',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class CreateUserComponent implements OnInit {
  hide = true;
  hideConf = true;
  buttonLabel = 'Register';
  matcher = new MyErrorStateMatcher();
  user: User;
  signUp: FormGroup;
  imgURL: string;
  fileToUpload: File = null;
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email
  ]);
  passwords: FormGroup;
  update = false;
  title = 'Register';
  private message: string;

  constructor(private formBuilderForm: FormBuilder,
              private formBuilderPasswords: FormBuilder,
              private loginCookieService: OurCookieService,
              private generalService: EndpointsService,
              private utilityService: UtilityService,
              private employeeService: UserService,
              private router: Router,
              private errorHandler: ErrorHandlerService,
              private snackBar: CustomSnackbarService) {
    this.signUp = this.formBuilderForm.group({
      name: [''],
      email: [''],
      category: [''],
      rating: ['']
    });
    if (this.router.url === '/account') {
      this.update = true;
      this.title = 'Update account';
    } else {
      this.passwords = this.formBuilderPasswords.group({
        passwordF: ['', [Validators.required]],
        passwordC: ['']
      }, {validator: this.checkPasswords});
    }
  }

  async ngOnInit() {
    if (this.update) {
      this.buttonLabel = 'Update';
      this.user = await this.employeeService.getUser();
      this.updateFormValues();
      this.imgURL = this.user.image;
    }
    if (!this.update && this.loginCookieService.getCookie(TsConstants.LOGGED_USER)) {
      this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
    }
  }

  private updateFormValues() {
    this.signUp.get('name').setValue(this.user.name);
    this.emailFormControl.setValue(this.user.email);
  }

  private checkPasswords(group: FormGroup) {
    if (!group || !group.controls || !group.controls.passwordC.value || !group.controls.passwordF.value) {
      return null;
    }
    const pass = group.controls.passwordF.value;
    const confirmPass = group.controls.passwordC.value;
    return pass === confirmPass ? null : {notSame: true};
  }

  preview(file: FileList) {
    if (file.length === 0) {
      return;
    }
    const mimeType = file[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.message = 'Only images are supported.';
      return;
    }
    this.fileToUpload = file.item(0);
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imgURL = event.target.result;
    };
    reader.readAsDataURL(this.fileToUpload);
  }

  onSubmit() {
    const user = this.populateUser();
    if (!this.update) {
      this.utilityService.post<User>(user, TsConstants.APP_ENDPOINTS.USERS).subscribe(
        success => {
          this.snackBar.successSnackBar('Your account has been created!');
          setTimeout(() => {
            this.router.navigate([TsConstants.ROUTES.LOGIN]);
          }, 2000);
        },
        error => {
          this.errorHandler.handleError(error);
        }
      );
    } else {
      this.generalService.update<User>(user, TsConstants.APP_ENDPOINTS.USERS).subscribe(
        success => {
          this.snackBar.successSnackBar('Your account has been updated!');
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

  private populateUser(): User {
    const signUpValues = this.signUp;
    const user: User = {
      name: signUpValues.value.name,
      email: this.emailFormControl.value,
      password: '',
      image: this.imgURL
    };
    if (this.update) {
      user.id = this.user.id;
    } else {
      user.password = this.passwords.value.passwordC;
    }
    return user;
  }
}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.touched);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.touched);
    return (invalidCtrl || invalidParent);
  }
}


