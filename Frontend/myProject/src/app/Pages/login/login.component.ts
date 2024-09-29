import { Component, inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../Services/user.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule,RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  ngOnInit(): void {
    console.log(this.isFormSubmit)    
  }


  Auth = inject(UserService)
  route = inject(Router)
  isFormSubmit:boolean = false;
  loginForm = new FormGroup({
    email: new FormControl("",[Validators.required,Validators.email]),
    password: new FormControl("",[Validators.required])
  })

  login():void{
    this.isFormSubmit = true
    if(this.loginForm){
        this.Auth.login(this.loginForm.value)
        .subscribe({
          next: response => {
            console.log(response)
            debugger
            this.route.navigateByUrl("/dashboard")
          },
          error: error => {
            console.error('Login failed:', error);
            alert("Invalid UserName or Password")
          }
        });
    }

  }

}
