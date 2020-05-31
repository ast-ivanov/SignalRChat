import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(private service: UserService) { }

  ngOnInit() {
  }

  get userName() { return this.service.formModel.get('UserName'); }

  get password() { return this.service.formModel.get('Passwords.Password'); }

  get confirmPassword() { return this.service.formModel.get('Passwords.ConfirmPassword'); }

  onSubmit() {
    this.service.register().subscribe(
      result => {},
      error => console.log(error)
    )
  }
}
