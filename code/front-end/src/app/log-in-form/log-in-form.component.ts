import {Component,  OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {emailDomainValidator} from '../validators/email-validator';
import {LoginService} from '../services/login.service';
import {Router} from '@angular/router';
import {OurCookieService} from '../services/shared/our-cookie.service';
import {TsConstants} from '../constants/tsConstants';
import {ErrorHandlerService} from '../services/error-handler.service';
import {ViewEncapsulation} from '@angular/cli/lib/config/schema';

@Component({
  selector: 'app-log-in-form',
  templateUrl: './log-in-form.component.html',
  styleUrls: ['./log-in-form.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class LogInFormComponent implements OnInit {
  hide = true;
  hideConf = true;
  loginForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30), emailDomainValidator]],
    password: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30)]]
  });

  get EmailFormControl() {
    return this.loginForm.get('email');
  }

  get PasswordFormControl() {
    return this.loginForm.get('password');
  }

  constructor(private formBuilder: FormBuilder,
              private loginService: LoginService,
              private router: Router,
              private loginCookieService: OurCookieService,
              private errorHandler: ErrorHandlerService) {}

  ngOnInit() {
    if (this.loginCookieService.getCookie(TsConstants.LOGGED_USER)) {
      this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
    }
  }

  onSubmit() {
    this.loginService.checkLogin(this.EmailFormControl.value, this.PasswordFormControl.value)
      .subscribe(
        response => {
            if (!response) {return; }
            this.loginCookieService.setCookie(response, TsConstants.LOGGED_USER);
            this.loginCookieService.setCookie(response.Id, TsConstants.LOGGED_USER);
            const cookie = this.loginCookieService.getCookie(TsConstants.LOGGED_USER);
            if (cookie.slice(cookie.length - 8, cookie.length) === TsConstants.ADMIN_TOKEN_END) {
              this.router.navigate([TsConstants.ROUTES.ADMIN]);
            } else {
              this.router.navigate([TsConstants.ROUTES.DASHBOARD]);
            }
          },
        error => {
          this.errorHandler.handleError(error);
        }
      );
  }

  goToRegister() {
    this.router.navigate([TsConstants.ROUTES.REGISTER]);
  }

  getErrorValidationMessageEmail() {
    return this.EmailFormControl.hasError('required') ? 'Email is required' :
      this.EmailFormControl.hasError('minlength') ? 'Email must have at least 3 characters' :
        this.EmailFormControl.hasError('maxLength') ? 'Email can not have over 30 characters' :
          this.EmailFormControl.hasError('emailvalidator') ? 'Email must be valid' :
            '';
  }

  getErrorValidationMessagePassword() {
    return this.PasswordFormControl.hasError('required') ? 'Password is required' :
      this.PasswordFormControl.hasError('minlength') ? 'Password must have at least 3 characters' :
        this.PasswordFormControl.hasError('maxLength') ? 'Password can not have over 30 characters' :
          '';
  }
}
