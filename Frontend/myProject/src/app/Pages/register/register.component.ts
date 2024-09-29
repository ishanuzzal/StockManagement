import { Component, inject } from '@angular/core';
import { UserService } from '../../Services/user.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import passwordValidator from '../../Validators/PasswordValidator';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  Auth = inject(UserService);
  router = inject(Router)
  isFormSubmit:boolean = false
  
  RegisterForm = new FormGroup({
    userName: new FormControl("",[Validators.required,Validators.minLength(5)]),
    email: new FormControl("",Validators.required),
    password: new FormControl("",[Validators.required,passwordValidator()])
  })
  
 

  register(){
    this.isFormSubmit = true
    if(this.RegisterForm.valid){
      this.Auth.register(this.RegisterForm.value)
      .subscribe({
        next: response=>{
          if(response){
            this.router.navigateByUrl("/login")
          }
          else{
            alert("registraion failed")
          }
        }
      })
    }

  }
}


