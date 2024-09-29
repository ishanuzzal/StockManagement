import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap, throwError, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http:HttpClient) { }
  url = "https://localhost:7028/api/User"

  login(data:any){
    return this.http.post(`${this.url}/login`,data)
    .pipe(tap((result) => {
      console.log(result)
      localStorage.setItem('authUser', JSON.stringify(result));
    }));
  }

  logout(){
    localStorage.removeItem('authUser');
  }

  isLoggedIn() {
    return localStorage.getItem('authUser') !== null;
  }

  register(data:any){
    return this.http.post(`${this.url}/register`,data)
  }
  

}
