import {AbstractControl} from '@angular/forms';

export function emailDomainValidator(control: AbstractControl): {[key: string]: any} {
  const email = control.value;
  if (email && email.indexOf('@') !== -1) {
    return null;
  }
  return { emailvalidator : {value: control.value}};
}
