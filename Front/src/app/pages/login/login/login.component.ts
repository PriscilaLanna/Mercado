import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms'

import { AuthService } from './../../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  public form:FormGroup = this.fb.group({});
  private formSubmitAttempt:boolean = false;

  constructor(private fb:FormBuilder,private authService:AuthService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      userName:['',Validators.required],
      password:['',Validators.required],
    });
  }

  isFieldInvalid(fieldName:string):boolean{
    const field = this.form.get(fieldName);
    if(field){
      return (!field.valid && field.touched) || (field.untouched && this.formSubmitAttempt);
    }

    return false;
  }

  onSubmit():void{
      if(this.form.valid){
        this.authService.login(this.form.value);
      }
      this.formSubmitAttempt = true;
  }

}
