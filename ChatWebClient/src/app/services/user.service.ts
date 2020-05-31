import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  
  private readonly BaseURL = 'https://localhost:44389/api/authentication'
  
  constructor(private fb: FormBuilder, private http: HttpClient) { }

  formModel = this.fb.group({
    UserName: ['', Validators.required],
    Passwords: this.fb.group({
      Password: ['', [Validators.required, Validators.minLength(4)]],
      ConfirmPassword: ['', Validators.required] 
    }, { validator: this.confirmPasswords })
  })

  confirmPasswords(fg: FormGroup)
  {
    let confirmPasswordControl = fg.get('ConfirmPassword')
    if (confirmPasswordControl.errors == null || 'passwordMismatch' in confirmPasswordControl.errors) {
      if (fg.get('Password').value != confirmPasswordControl.value) {
        confirmPasswordControl.setErrors({ passwordMismatch: true })
      } else {
        confirmPasswordControl.setErrors(null);
      }
    }
  }

  public register() {
    let body = {
      UserName: this.formModel.value.UserName,
      Password: this.formModel.value.Password
    }

    return this.http.post(`${this.BaseURL}/register`, body);
  }

  public login(formData) {
    return this.http.post(`${this.BaseURL}/login`, formData);
  }
}
